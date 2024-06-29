using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Identity.DbContext
{
    public class HrLeavemanagementIdentityDbContext
        : IdentityDbContext<ApplicationUser>
    {
        public HrLeavemanagementIdentityDbContext(DbContextOptions<HrLeavemanagementIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(HrLeavemanagementIdentityDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
