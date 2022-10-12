using LeaveManagement.Web.Constants;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using Microsoft.AspNetCore.Identity;

namespace LeaveManagement.Web.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly UserManager<Employee> userManager;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public LeaveAllocationRepository(ApplicationDbContext context, 
            UserManager<Employee> userManager, ILeaveTypeRepository leaveTypeRepository): base(context)
        {
            this.userManager = userManager;
            this.leaveTypeRepository = leaveTypeRepository;
        }

        public async Task LeaveAllocation(int leaveTypeId)
        {
            //get all employees in range so we can allocate them the leave:
            var employees = await userManager.GetUsersInRoleAsync(Roles.User);
            var period = DateTime.Now.Year;
            var leaveType = await leaveTypeRepository.GetAsync(leaveTypeId);


            foreach (var employee in employees)
            {
                var allocation = new LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    Period = period,
                    LeaveTypeId = leaveTypeId,
                    NumberOfDays = leaveType.DefaultDays


                };
            }

            throw new NotImplementedException();
        }
    }
}
