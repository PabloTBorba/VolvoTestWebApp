#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolvoTestWebApp.Data;
using VolvoTestWebApp.Helpers;
using VolvoTestWebApp.Models;

namespace VolvoTestWebApp.Controllers
{
    public class VolvoTrucksController : Controller
    {
        private readonly VolvoTestWebAppContext _context;

        public VolvoTrucksController(VolvoTestWebAppContext context)
        {
            _context = context;
        }

        #region Private methods

        private async Task<List<VolvoTruckModel>> GetTrucks()
        {
            return await _context.VolvoTrucks.ToListAsync();
        }

        private async Task<VolvoTruckModel> GetTruckById(int? id)
        {
            if (!id.HasValue)
                return null;

            return await _context.VolvoTrucks
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<UpdateTypeEnum> UpsertTruck(VolvoTruckModel volvoTruckModel)
        {
            if (volvoTruckModel.IsEdit)
                _context.Update(volvoTruckModel);
            else
                _context.Add(volvoTruckModel);

            return await SaveChanges(true, volvoTruckModel.IsEdit);
        }

        private async Task<UpdateTypeEnum> DeleteTruck(int truckId)
        {
            var volvoTruckModel = await GetTruckById(truckId);
            _context.VolvoTrucks.Remove(volvoTruckModel);
            return await SaveChanges(false);
        }

        private async Task<UpdateTypeEnum> SaveChanges(bool isUpsert, bool? isEdit = false)
        {
            try
            {
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                    if (isUpsert)
                    {
                        return (isEdit.Value) ?
                            UpdateTypeEnum.UpdateOk :
                            UpdateTypeEnum.CreateOk;
                    }
                    else
                        return UpdateTypeEnum.DeleteOk;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return UpdateTypeEnum.NoResult;
        }

        #endregion Private methods

        #region GET methods

        /**
         * GET: VolvoTrucks
         * Returns the list of registered trucks - if exists
         */
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trucks = await GetTrucks();
            return View(trucks);
        }

        // GET: VolvoTrucks/Create
        [HttpGet]
        public IActionResult Create()
        {
            var model = new VolvoTruckModel
            {
                IsEdit = false
            };
            return View(model);
        }

        // GET: VolvoTrucks/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var volvoTruckModel = await GetTruckById(id);
            volvoTruckModel.IsEdit = true;

            if (volvoTruckModel == null)
                return NotFound();

            return View(volvoTruckModel);
        }

        // GET: VolvoTrucks/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var volvoTruckModel = await GetTruckById(id);

            if (volvoTruckModel == null)
                return NotFound();

            return View(volvoTruckModel);
        }

        #endregion GET methods

        #region POST methods

        // POST: VolvoTrucks/UpdateTruck/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTruck(int? id, [Bind("Id,Description,Model," +
            "ModelYear,IsEdit")] VolvoTruckModel volvoTruckModel)
        {
            if (ModelState.IsValid)
            {
                await UpsertTruck(volvoTruckModel);
                return RedirectToAction(nameof(Index));
            }

            return View(volvoTruckModel);
        }


        // POST: VolvoTrucks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await DeleteTruck(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion POST methods
    }
}
