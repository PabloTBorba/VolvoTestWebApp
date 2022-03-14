using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using VolvoTestWebApp.Data;

namespace VolvoTestWebApp.Entities
{
    /**
     * 
     */
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<VolvoTestWebAppContext>())
                {
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
