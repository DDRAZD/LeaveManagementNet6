﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using LeaveManagement.Application.Contracts;
using LeaveManagement.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using LeaveManagement.Common.Models;

namespace LeaveManagement.Application.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<Employee> userManager;
        private readonly AutoMapper.IConfigurationProvider configurationProvider;
        private readonly IEmailSender emailSender;

        public LeaveRequestRepository(ApplicationDbContext context, 
            IMapper mapper,
            ILeaveAllocationRepository leaveAllocationRepository, 
            IHttpContextAccessor httpContextAccessor, 
            UserManager<Employee> userManager, 
            AutoMapper.IConfigurationProvider configurationProvider,
            IEmailSender emailSender) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.configurationProvider = configurationProvider;
            this.emailSender = emailSender;
        }

        public async Task CancelLeaveRequest(int leaveRequestId)
        {
            var leaveRequest = await GetAsync(leaveRequestId);
            leaveRequest.Cancelled = true;
            await UpdateAsync(leaveRequest);



           // var user = await userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId);
            //var approvalStatus = "Canceled";

           // await emailSender.SendEmailAsync(user.Email, $"Leave Request {approvalStatus}", $"your leave request from " + $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been {approvalStatus}");
        }

        /// <summary>
        /// updates the allocation - after you approve a leave, the allocation has to drop as you used it
        /// </summary>
        /// <param name="leaveRequestId"></param>
        /// <param name="approved"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ChangeApprovalStatus(int leaveRequestId, bool approved)
        {
            var leaveRequest = await GetAsync(leaveRequestId);
            leaveRequest.Approved = approved;//approve the request

            if(approved ==true)
            {
                var allocation = await leaveAllocationRepository.GetEmployeeAllocation(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                allocation.NumberOfDays = allocation.NumberOfDays - daysRequested;

                await leaveAllocationRepository.UpdateAsync(allocation);
            }
            await UpdateAsync(leaveRequest);

          /*  var user = await userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId);

            var approvalStatus = approved ? "Approved" : "Declined";

            await emailSender.SendEmailAsync(user.Email, $"Leave Request {approvalStatus}", $"your leave request from " + $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been {approvalStatus}");*/
        }

        public async Task<bool> CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);

           var allocations =  await leaveAllocationRepository.GetEmployeeAllocation(user.Id,model.LeaveTypeId);

            //the view model validations themselves (MoodelState.IsValid) gurantee start and end date are not null here
            int requestedDuration = (int)(model.EndDate.Value - model.StartDate.Value).TotalDays;
           
            if(allocations!=null)
            {
                if(allocations.NumberOfDays< requestedDuration)
                    return false;
            }
            else
            {
                return false; //allocations is null
            }
            //allocations is not null and it is larger than duration requested:

            var leaveRequest = mapper.Map<LeaveRequest>(model);
            leaveRequest.DateRequested = DateTime.Now;
            leaveRequest.Cancelled = false;
            leaveRequest.RequestingEmployeeId = user.Id;
            await this.AddAsync(leaveRequest);

          // await  emailSender.SendEmailAsync(user.Email, "Request Created", $"your leave request from " + $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been submitted for approval");

            return true;
            
        }

        public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList()
        {
            var leaveRequests = await context.LeaveRequests.Include(q=>q.LeaveType).ToListAsync(); // get all the leave requests
            var model = new AdminLeaveRequestViewVM();

            model.TotalRequests=leaveRequests.Count;
            model.RejectedRequests = leaveRequests.Count(y => y.Approved == false);
            model.ApprovedRequests = leaveRequests.Count(y=>y.Approved==true);
            model.PendingRequests = leaveRequests.Count(y => y.Approved == null);//assuming canceled will be false in the Approved field
            model.LeaveRequests = mapper.Map<List<LeaveRequestVM>>(leaveRequests);
            foreach(var leaveRequest in model.LeaveRequests)
            {
                leaveRequest.Employee =  mapper.Map<EmployeeListVM>(await userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId));
            }

            return model;
        }

        public async Task<List<LeaveRequestVM>> GetAllAsync(string employeedId)
        {
            var requests = await context.LeaveRequests.Where(a => a.RequestingEmployeeId == employeedId)
                .ProjectTo<LeaveRequestVM>(configurationProvider)
                .ToListAsync();
            //var model =  mapper.Map<List<LeaveRequestVM>>(requests);
            return requests;
        }

        public async Task<LeaveRequestVM> GetLeaveRequestAsync(int? id)
        {
            var leaveRequest = await context.LeaveRequests.Include(q=>q.LeaveType).FirstOrDefaultAsync(a => a.Id == id);

            if(leaveRequest == null)
            {
                return null;
            }    
            var model = mapper.Map<LeaveRequestVM>(leaveRequest);
            model.Employee =mapper.Map<EmployeeListVM> (await userManager.FindByIdAsync(leaveRequest?.RequestingEmployeeId));
            return model;
        }

        public async Task<EmployeeLeaveRequestViewVM> GetMyLeaveDetails()
        {
            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);
            var leaveAllocations =  (await leaveAllocationRepository.GetEmployeeAllocations(user.Id)).LeaveAllocations; 
            var requestsVM = await GetAllAsync(user.Id);
           // var requestsVM =  mapper.Map<List<LeaveRequestVM>>(requests);

            EmployeeLeaveRequestViewVM model = new EmployeeLeaveRequestViewVM()
            {
                LeaveAllocations = leaveAllocations,
                LeaveRequests = requestsVM
            };
            return model;


        }
    }
}
