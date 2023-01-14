using LeaveManagement.Web.Contracts;
using LeaveManagement.Data;

namespace LeaveManagement.Web.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(ApplicationDbContext context) : base(context)
        {

        }

        
    }
}
