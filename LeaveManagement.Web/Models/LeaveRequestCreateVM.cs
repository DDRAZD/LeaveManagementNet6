using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Web.Models
{
    public class LeaveRequestCreateVM
    {
        [Display(Name ="Leave Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Leave End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name ="Leave Types")]
        public int LeaveTypeId { get; set; } //useful for dropdown list
        public SelectList LeaveTypes { get; set; } //for the drop down list

      //  public DateTime DateRequested { get; set; }

        [Display(Name ="Comments")]
        public string RequestComments { get; set; }

      //  public string RequestingEmployeeId { get; set; }

        //public bool? Approved { get; set; }

      //  public bool Cancelled { get; set; }
    }
}
