using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Web.Models
{
    public class LeaveAllocationVM
    {
        [Required]
        public int Id { get; set; } 

        [Display(Name ="Number of Days")]
        [Required]
        [Range(1,100, ErrorMessage ="invalid number entered for number of days")]
        public int NumberOfDays { get; set; }


        public LeaveTypeVM? LeaveType { get; set; }    
       
        [Display (Name ="Allocation Period")]
        [Required]
        public int Period { get; set; } 
    }
}