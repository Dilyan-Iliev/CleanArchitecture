using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository
        : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(HrDatabaseContext db)
            : base(db)
        {
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequests = await _db.LeaveRequests
                .Include(lr => lr.LeaveType)
                .ToListAsync();

            return leaveRequests;
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
        {
            var leaveRequests = await _db.LeaveRequests
                .Where(lr => lr.RequestingEmployeeId == userId)
                .Include(lr => lr.LeaveType)
                .ToListAsync();

            return leaveRequests;
        }

        public async Task<LeaveRequest?> GetLeaveRequestWithDetails(int id)
        {
            return await _db.LeaveRequests
                .Include(lr => lr.LeaveType)
                .FirstOrDefaultAsync(lr => lr.Id == id);
        }
    }
}
