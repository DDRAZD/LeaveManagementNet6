﻿using AutoMapper;
using LeaveManagement.Web.Constants;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Employee> userManager;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;

        public LeaveAllocationRepository(ApplicationDbContext context, 
            UserManager<Employee> userManager, ILeaveTypeRepository leaveTypeRepository, IMapper mapper): base(context)
        {
            this.context = context;
            this.userManager = userManager;
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
        }

        public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
        {
            return await context.LeaveAllocations.AnyAsync(a => a.EmployeeId == employeeId && a.LeaveTypeId == leaveTypeId && a.Period == period);

        }

        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string employeeId)
        {
           var allocations = await context.LeaveAllocations
                .Include(a => a.LeaveType).Where(y=>y.EmployeeId==employeeId).ToListAsync();


           var employee = await userManager.FindByIdAsync(employeeId);
           var employeeAllocationModel = mapper.Map<EmployeeAllocationVM>(employee);//all the fields that match will be mapped
            //still need to do it for allocations

            //now mapping the allocations:
            employeeAllocationModel.LeaveAllocations = mapper.Map<List<LeaveAllocationVM>>(allocations);
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
