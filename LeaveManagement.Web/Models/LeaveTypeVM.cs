using System.ComponentModel.DataAnnotations;


namespace LeaveManagement.Web.Models
{
    public class LeaveTypeVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name ="Default Number of Days")]
        public int DefaultDays { get; set; }

    }
}
