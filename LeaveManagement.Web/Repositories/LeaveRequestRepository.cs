using AutoMapper;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<Employee> userManager;

        public LeaveRequestRepository(ApplicationDbContext context, IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository, IHttpContextAccessor httpContextAccessor, UserManager<Employee> userManager) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task CancelLeaveRequest(int leaveRequestId)
        {
            var leaveRequest = await GetAsync(leaveRequestId);
            leaveRequest.Cancelled = true;
            await UpdateAsync(leaveRequest);
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

        public async Task<List<LeaveRequest>> GetAllAsync(string employeedId)
        {
            var requests = await context.LeaveRequests.Where(a => a.RequestingEmployeeId == employeedId).ToListAsync();
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
            var requests = await GetAllAsync(user.Id);
            var requestsVM =  mapper.Map<List<LeaveRequestVM>>(requests);

            EmployeeLeaveRequestViewVM model = new EmployeeLeaveRequestViewVM()
            {
                LeavLeaveAllocations = leaveAllocations,
                LeaveRequests = requestsVM
            };
            return model;


        }
    }
}
