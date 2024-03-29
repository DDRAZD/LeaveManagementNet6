﻿using AutoMapper;
using LeaveManagement.Data;
using LeaveManagement.Common.Models;

namespace LeaveManagement.Application.Configuration
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
            CreateMap<LeaveRequestCreateVM, LeaveRequest>().ReverseMap();
            CreateMap<LeaveRequestVM, LeaveRequest>().ReverseMap();


        }
    }
}
