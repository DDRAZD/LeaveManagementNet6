using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Web.Models
{
    public class LeaveRequestCreateVM:IValidatableObject
    {
        [Display(Name ="Leave Start Date")]
        [Required]
        public DateTime? StartDate { get; set; }
        [Display(Name = "Leave End Date")]
        [Required]
        public DateTime? EndDate { get; set; }

        [Display(Name ="Leave Types")]
        [Required]
        public int LeaveTypeId { get; set; } //useful for dropdown list
        public SelectList? LeaveTypes { get; set; } //for the drop down list

      //  public DateTime DateRequested { get; set; }

        [Display(Name ="Comments")]
       
        public string? RequestComments { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(StartDate > EndDate)
            {
                yield return new ValidationResult("the start date cannot be after end date",new[] {nameof(StartDate), nameof(EndDate)});
            }
            if(RequestComments != null)
            {
                if (RequestComments.Length > 159)
                {
                    yield return new ValidationResult("comment too long", new[] { nameof(RequestComments) });
                }
            }
            
        }

        //  public string RequestingEmployeeId { get; set; }

        //public bool? Approved { get; set; }

        //  public bool Cancelled { get; set; }
    }
}
