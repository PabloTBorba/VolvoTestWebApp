using Microsoft.EntityFrameworkCore;
using VolvoTestWebApp.Data.Repositories.Abstractions;
using VolvoTestWebApp.Models;

namespace VolvoTestWebApp.Data.Repositories
{
    public class VolvoTruckRepository : IVolvoTruckRepository
    {
        private readonly VolvoTestWebAppContext _context;

        public VolvoTruckRepository(VolvoTestWebAppContext context)
        {
            _context = context;
        }

        private async Task SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<VolvoTruckModel>> GetTrucks()
        {
            return await _context.VolvoTrucks.ToListAsync();
        }

        public async Task<VolvoTruckModel> GetTruckById(int? id)
        {
            if (!id.HasValue)
                return null;

            return await _context.VolvoTrucks
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpsertTruck(VolvoTruckModel volvoTruckModel)
        {
            if (volvoTruckModel.IsEdit)
                _context.Update(volvoTruckModel);
            else
                _context.Add(volvoTruckModel);

            await SaveChanges();
        }

        public async Task DeleteTruck(int truckId)
        {
            var volvoTruckModel = await GetTruckById(truckId);
            _context.VolvoTrucks.Remove(volvoTruckModel);
            await SaveChanges();
        }
    }
}
