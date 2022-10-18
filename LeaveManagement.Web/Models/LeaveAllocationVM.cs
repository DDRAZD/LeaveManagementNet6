namespace LeaveManagement.Web.Models
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; } 
        public int NumberOfDays { get; set; }


        public LeaveTypeVM LeaveType { get; set; }    
       

        public int Period { get; set; } 
    }
}