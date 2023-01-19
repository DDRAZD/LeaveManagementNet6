namespace LeaveManagement.Common.Models
{
    public class EmployeeLeaveRequestViewVM
    {
        public List<LeaveAllocationVM> LeaveAllocations { get;  set; }
       

        public List<LeaveRequestVM>? LeaveRequests { get; set; }
    }
}
