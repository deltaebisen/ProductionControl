using Masters.Models;
using Microsoft.EntityFrameworkCore;

namespace Masters.Infrastructures
{
    public class DbInitializer
    {
        public static void CreateDbIfNotExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<MasterDbContext>();
                //context.Database.EnsureCreated();
                Seed(context);
            }
            catch ( Exception ex )
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
        private static void Seed(MasterDbContext context)
        {
            context.Database.EnsureCreated();
            if ( !context.DepartmentHistories.Any() )
            {
                var departmentHistories = new DepartmentHistory[]
                {
                new DepartmentHistory("A", 1, "部門A", false, DateTime.Now),
                new DepartmentHistory("B", 1, "部門B", false, DateTime.Now),
                new DepartmentHistory("C", 1, "部門C", false, DateTime.Now),
                new DepartmentHistory("D", 1, "部門D", false, DateTime.Now),
                new DepartmentHistory("E", 1, "部門E", false, DateTime.Now)
                };
                context.DepartmentHistories.AddRange(departmentHistories);
            }
            context.SaveChanges();
        }
    }
}
