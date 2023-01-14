using AutoMapper;
using AutoMapper.QueryableExtensions;
using LeaveManagement.Common.Constants;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Data;
using LeaveManagement.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Employee> userManager;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;
        private readonly AutoMapper.IConfigurationProvider configurationProvider;

        public LeaveAllocationRepository(ApplicationDbContext context, 
            UserManager<Employee> userManager, 
            ILeaveTypeRepository leaveTypeRepository,             
            AutoMapper.IConfigurationProvider configurationProvider,
            IMapper mapper,
            IEmailSender emailSender) : base(context)
        {
            this.context = context;
            this.userManager = userManager;
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
            this.emailSender = emailSender;
            this.configurationProvider = configurationProvider;
        }

        public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
        {
            return await context.LeaveAllocations.AnyAsync(a => a.EmployeeId == employeeId && a.LeaveTypeId == leaveTypeId && a.Period == period);

        }

        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string employeeId)
        {
           var allocations = await context.LeaveAllocations
                .Include(a => a.LeaveType)
                .Where(y=>y.EmployeeId==employeeId)
                .ProjectTo<LeaveAllocationVM>(configurationProvider)
                .ToListAsync();


           var employee = await userManager.FindByIdAsync(employeeId);
           var employeeAllocationModel = mapper.Map<EmployeeAllocationVM>(employee);//all the fields that match will be mapped
            //still need to do it for allocations

            //now mapping the allocations:
            //employeeAllocationModel.LeaveAllocations = mapper.Map<List<LeaveAllocationVM>>(allocations);
            employeeAllocationModel.LeaveAllocations = allocations;//the ProjecTo already provides the mapping
            return employeeAllocationModel;
        }

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int Id)
        {
            var allocations = await context.LeaveAllocations
                 .Include(a => a.LeaveType).FirstOrDefaultAsync(q=>q.Id== Id);


           if(allocations==null)
            {
                return null;
            }

           var employee = await userManager.FindByIdAsync(allocations.EmployeeId);
            var model = mapper.Map<LeaveAllocationEditVM>(allocations);
            model.Employee = mapper.Map<EmployeeListVM>(employee);

            return model;
        }

        public async Task LeaveAllocation(int leaveTypeId)
        {
            //get all employees in range so we can allocate them the leave:
            var employees = await userManager.GetUsersInRoleAsync(Roles.User);
            var period = DateTime.Now.Year;
            var leaveType = await leaveTypeRepository.GetAsync(leaveTypeId);

            List<LeaveAllocation> allocations = new List<LeaveAllocation>();

            var employeesWithNewAllocaitons = new List<Employee>();

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
                employeesWithNewAllocaitons.Add(employee);
                
              
            }
            await AddRangeAsync(allocations);

            foreach(var employee in employeesWithNewAllocaitons)
            {
                
                await emailSender.SendEmailAsync(employee.Email, $"Leave allocation posted for {period}", $"Your {leaveType.Name} leave " +
                    $"has been posted for the period {period}. You have been given {leaveType.DefaultDays}");
            }


            
        }

        public async Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditVM model)
        {
            var leaveAllocation = await this.GetAsync(model.Id);
            if (leaveAllocation == null)
            {
                return false;
            }
            leaveAllocation.Period = model.Period;
            leaveAllocation.NumberOfDays = model.NumberOfDays;
            await this.UpdateAsync(leaveAllocation);
            return true;
        }

        public async Task<LeaveAllocation?> GetEmployeeAllocation(string employeeId, int Id)
        {
            var allocations = await context.LeaveAllocations
               .Include(a => a.LeaveType).Where(y => y.EmployeeId == employeeId).ToListAsync(); //gets all types of allocations for the employee (sick, vacation etc)


            

            
            var allocation =  allocations.FirstOrDefault(y=>y.LeaveTypeId==Id); //pulls the specfic allocation (such as allocation to sick leave)
            return allocation;
        }
    }
}
