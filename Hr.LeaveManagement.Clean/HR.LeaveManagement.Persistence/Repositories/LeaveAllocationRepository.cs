using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository
        : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext db)
            : base(db)
        {
        }

        public async Task AddAllocations(IEnumerable<LeaveAllocation> allocations)
        {
            await _db.LeaveAllocations.AddRangeAsync(allocations);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _db.LeaveAllocations
                .AnyAsync(la => la.EmployeeId == userId
                    && la.LeaveTypeId == leaveTypeId
                    && la.Period == period);
        }

        public async Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var leaveAllocations = await _db.LeaveAllocations
                .Include(la => la.LeaveType)
                .ToListAsync();

            return leaveAllocations;
        }

        public async Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            var leaveAllocations = await _db.LeaveAllocations
                .Where(la => la.EmployeeId == userId)
                .Include(la => la.LeaveType)
                .ToListAsync();

            return leaveAllocations;
        }

        public async Task<LeaveAllocation?> GetLeaveAllocationWithDetails(int id)
        {
            return await _db.LeaveAllocations
                .Include(la => la.LeaveType)
                .FirstOrDefaultAsync(la => la.Id == id);
        }

        public async Task<LeaveAllocation?> GetUserAllocations(string userId, int leaveTypeId)
        {
            return await _db.LeaveAllocations
                .FirstOrDefaultAsync(la => la.EmployeeId == userId
                    && la.LeaveTypeId == leaveTypeId);
        }
    }
}
