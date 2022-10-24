using AutoMapper;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Configuration
{
    public class MapperConfig: Profile
    {
        //constructor
        public MapperConfig()
        {
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap(); //allows conversion from model to view model (from data to UI) while "ReverseMap" adds
                                                              //..the reverse conversion from VM back to Data model

            CreateMap<Employee,EmployeeListVM >().ReverseMap();
            CreateMap<Employee, EmployeeAllocationVM>().ReverseMap();

            CreateMap<LeaveAllocation, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationEditVM>().ReverseMap();


        }
    }
}
