#nullable disable
using Microsoft.AspNetCore.Mvc;
using VolvoTestWebApp.Data.Repositories.Abstractions;
using VolvoTestWebApp.Models;

namespace VolvoTestWebApp.Controllers
{
    public class VolvoTrucksController : Controller
    {
        private readonly IVolvoTruckRepository _repo;

        public VolvoTrucksController(IVolvoTruckRepository repo)
        {
            _repo = repo;
        }

        #region GET methods

        /**
         * GET: VolvoTrucks
         * Returns the list of registered trucks - if exists
         */
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trucks = await _repo.GetTrucks();
            return View(trucks);
        }

        /**
         * GET: VolvoTrucks/Create
         * Register a new truck in the system
         */
        [HttpGet]
        public IActionResult Create()
        {
            var model = new VolvoTruckModel
            {
                IsEdit = false
            };
            return View(model);
        }

        /**
         * GET: VolvoTrucks/Edit/5
         * Get the info from a existing truck for update purposes
         */
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var volvoTruckModel = await _repo.GetTruckById(id);
            volvoTruckModel.IsEdit = true;

            if (volvoTruckModel == null)
                return NotFound();

            return View(volvoTruckModel);
        }

        /**
         * GET: VolvoTrucks/Delete/5
         * Erase a truck from the system
         */
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var volvoTruckModel = await _repo.GetTruckById(id);

            if (volvoTruckModel == null)
                return NotFound();

            return View(volvoTruckModel);
        }

        #endregion GET methods

        #region POST methods

        /**
         * POST: VolvoTrucks/UpdateTruck/5
         * The same post method is used for both create and edit forms
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTruck(int? id, [Bind("Id,Description,Model," +
            "ModelYear,IsEdit")] VolvoTruckModel volvoTruckModel)
        {
            if (ModelState.IsValid)
            {
                await _repo.UpsertTruck(volvoTruckModel);
                return RedirectToAction(nameof(Index));
            }

            return View(volvoTruckModel);
        }


        // POST: VolvoTrucks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteTruck(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion POST methods
    }
}
