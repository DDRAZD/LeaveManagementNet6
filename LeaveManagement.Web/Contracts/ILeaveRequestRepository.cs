using LeaveManagement.Data;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Contracts
{
    public interface ILeaveRequestRepository:IGenericRepository<LeaveRequest>
    {
        Task<bool> CreateLeaveRequest(LeaveRequestCreateVM model);

        Task<EmployeeLeaveRequestViewVM> GetMyLeaveDetails();
        Task<LeaveRequestVM?> GetLeaveRequestAsync(int? id);

        Task CancelLeaveRequest(int leaveRequestId);

        Task<List<LeaveRequestVM>> GetAllAsync(string employeedId); //overloading the generic method

        Task ChangeApprovalStatus(int leaveRequestId, bool approved);
        Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList();

    }
}
