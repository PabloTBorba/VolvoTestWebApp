using VolvoTestWebApp.Models;

namespace VolvoTestWebApp.Data.Repositories.Abstractions
{
    public interface IVolvoTruckRepository
    {
        /**
         * Return all trucks registered
         */
        Task<List<VolvoTruckModel>> GetTrucks();

        /**
         * Return a single truck that has the same ID passed by param
         */
        Task<VolvoTruckModel> GetTruckById(int? id);

        /**
         * Method to insert or update a truck based on the model passed by param
         */
        Task UpsertTruck(VolvoTruckModel volvoTruckModel);

        /**
         * Delete from the database the truck that has the same ID as the truckId param
         */
        Task DeleteTruck(int truckId);
    }
}
