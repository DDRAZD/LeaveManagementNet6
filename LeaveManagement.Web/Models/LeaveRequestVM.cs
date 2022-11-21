namespace LeaveManagement.Web.Models
{
    public class LeaveRequestVM
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

       
        public LeaveTypeVM LeaveType { get; set; }

        

        public DateTime DateRequested { get; set; }

        public string? RequestComments { get; set; }

        

        public bool? Approved { get; set; }

        public bool Cancelled { get; set; }
    }
}
