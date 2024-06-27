using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagement.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<HrDatabaseContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("HrDatabaseConnectionString"));
            });

            return services;
        }
    }
}
