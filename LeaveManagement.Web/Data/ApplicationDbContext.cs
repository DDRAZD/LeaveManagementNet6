using LeaveManagement.Web.Configuration.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleSeedConfiguration());//seeding roles
            builder.ApplyConfiguration(new UserSeedConfiguration());//seeding some users
            builder.ApplyConfiguration(new UserRolesSeedConfiguration());//assigning the users to role
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        // public DbSet<LeaveManagement.Web.Models.LeaveAllocationEditVM> LeaveAllocationEditVM { get; set; }
        // public DbSet<LeaveManagement.Web.Models.EmployeeAllocationVM> EmployeeAllocationVM { get; set; }
        //public DbSet<LeaveManagement.Web.Models.EmployeeListVM> EmployeeListVM { get; set; }



    }
}