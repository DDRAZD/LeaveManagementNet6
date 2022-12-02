using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Web.Models
{
    public class LeaveRequestVM: LeaveRequestCreateVM
    {
        public int Id { get; set; }
       
        [Display(Name = "Leave Type")]
        public LeaveTypeVM LeaveType { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Date Requested")]
        public DateTime DateRequested { get; set; }

        public bool? Approved { get; set; }

        public bool Cancelled { get; set; }

        public string? RequestingEmployeeId { get; set; }

        public EmployeeListVM Employee { get; set; } 
    }
}
