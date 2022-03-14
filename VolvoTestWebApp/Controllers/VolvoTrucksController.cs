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

            try
            {
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                    return (volvoTruckModel.IsEdit) ? 
                        UpdateTypeEnum.UpdateOk : 
                        UpdateTypeEnum.CreateOk;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return UpdateTypeEnum.NoResult;
        }

        #endregion Private methods

        #region GET methods

        // GET: VolvoTrucks
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
            var volvoTruckModel = await GetTruckById(id);
            _context.VolvoTrucks.Remove(volvoTruckModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /*
        // POST: VolvoTrucks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Model,FabricationYear,ModelYear")] VolvoTruckModel volvoTruckModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volvoTruckModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(volvoTruckModel);
        }

        // POST: VolvoTrucks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Model,FabricationYear,ModelYear")] VolvoTruckModel volvoTruckModel)
        {
            if (id != volvoTruckModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volvoTruckModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolvoTruckModelExists(volvoTruckModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(volvoTruckModel);
        }
        */

        #endregion POST methods

        /*
        // GET: VolvoTrucks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();



            if (volvoTruckModel == null)
            {
                return NotFound();
            }

            return View(volvoTruckModel);
        }
        */
    }
}
