using LeaveManagement.Data.Configuration.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LeaveManagement.Data
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

        /// <summary>
        /// whenever save changes will be called in the repository, the below save changes will be used
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            /*as part of entity frameowkr, base.ChangeTracker tracks which entities are changing
              to allow to do UpdateDB and SaveChanges etc. We are simply looping among all the
            entities that are changed and as we implemented inheritance in all of them from base entity, we are going after them
            by base entity; this loop will allow to always save the changes and modified date
           */
            foreach(var item in base.ChangeTracker.Entries<BaseEntity>().Where(q=>q.State ==EntityState.Added || q.State ==EntityState.Modified))
            {
                item.Entity.DateModified = DateTime.Now;
                if(item.State == EntityState.Added)//only happens the first time this entity is being created
                {
                    item.Entity.DateCreated = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        // public DbSet<LeaveManagement.Web.Models.LeaveAllocationEditVM> LeaveAllocationEditVM { get; set; }
        // public DbSet<LeaveManagement.Web.Models.EmployeeAllocationVM> EmployeeAllocationVM { get; set; }
        //public DbSet<LeaveManagement.Web.Models.EmployeeListVM> EmployeeListVM { get; set; }



    }
}