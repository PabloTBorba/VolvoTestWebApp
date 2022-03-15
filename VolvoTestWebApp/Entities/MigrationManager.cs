using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using VolvoTestWebApp.Data;

namespace VolvoTestWebApp.Entities
{
    public static class MigrationManager
    {
        /**
         * The IHost extension implemented here
         * will execute the migrations required on the system first execution (Program.app.Run())
         */
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<VolvoTestWebAppContext>())
                {
                    //The migration will run only if the context doesn't have the database available
                    if (!appContext.Database.GetService<IRelationalDatabaseCreator>().Exists())
                    {
                        try
                        {
                            appContext.Database.Migrate();
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }

            return host;
        }
    }
}
