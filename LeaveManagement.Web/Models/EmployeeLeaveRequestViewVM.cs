namespace LeaveManagement.Web.Models
{
    public class EmployeeLeaveRequestViewVM
    {
        public List<LeaveAllocationVM> LeavLeaveAllocations { get; internal set; }
       

        public List<LeaveRequestVM>? LeaveRequests { get; set; }
    }
}
