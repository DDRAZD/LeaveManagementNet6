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

        public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);
            var leaveRequest = mapper.Map<LeaveRequest>(model);
            leaveRequest.DateRequested = DateTime.Now;
            leaveRequest.Cancelled = false;
            leaveRequest.RequestingEmployeeId = user.Id;
            await this.AddAsync(leaveRequest);
            
        }

        public async Task<List<LeaveRequest>> GetAllAsync(string employeedId)
        {
            var requests = await context.LeaveRequests.Where(a => a.RequestingEmployeeId == employeedId).ToListAsync();
            return requests;
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
