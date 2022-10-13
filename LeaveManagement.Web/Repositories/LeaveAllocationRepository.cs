using LeaveManagement.Web.Constants;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Employee> userManager;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public LeaveAllocationRepository(ApplicationDbContext context, 
            UserManager<Employee> userManager, ILeaveTypeRepository leaveTypeRepository): base(context)
        {
            this.context = context;
            this.userManager = userManager;
            this.leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
        {
            return await context.LeaveAllocations.AnyAsync(a => a.EmployeeId == employeeId && a.LeaveTypeId == leaveTypeId && a.Period == period);

        }

        public async Task LeaveAllocation(int leaveTypeId)
        {
            //get all employees in range so we can allocate them the leave:
            var employees = await userManager.GetUsersInRoleAsync(Roles.User);
            var period = DateTime.Now.Year;
            var leaveType = await leaveTypeRepository.GetAsync(leaveTypeId);

            List<LeaveAllocation> allocations = new List<LeaveAllocation>();

            foreach (var employee in employees)
            {
                if (await AllocationExists(employee.Id, leaveTypeId, period))
                    continue;//goes to the next iteration and skips the add below

                allocations.Add(new LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    Period = period,
                    LeaveTypeId = leaveTypeId,
                    NumberOfDays = leaveType.DefaultDays                   
                });
                
              
            }
            await AddRangeAsync(allocations);
            
        }
    }
}
