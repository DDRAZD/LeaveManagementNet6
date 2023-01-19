using LeaveManagement.Data;
using LeaveManagement.Common.Models;

namespace LeaveManagement.Application.Contracts
{
    public interface ILeaveAllocationRepository: IGenericRepository<LeaveAllocation>
    {

        Task LeaveAllocation(int leaveTypeId);
        Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period);

        Task<EmployeeAllocationVM> GetEmployeeAllocations(string employeeId);

        Task<LeaveAllocation?> GetEmployeeAllocation(string employeeId, int Id);

        Task<LeaveAllocationEditVM> GetEmployeeAllocation(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>bool that is false if you cannot find the allocation in the database</returns>
        Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditVM model);
    }
}
