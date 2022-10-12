using LeaveManagement.Web.Data;

namespace LeaveManagement.Web.Contracts
{
    public interface ILeaveAllocationRepository: IGenericRepository<LeaveAllocation>
    {

        Task LeaveAllocation(int leaveTypeId);
    }
}
