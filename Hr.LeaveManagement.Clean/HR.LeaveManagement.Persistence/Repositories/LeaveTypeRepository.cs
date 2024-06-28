using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository
        : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrDatabaseContext db) 
            : base(db)
        {
        }

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            return await _db.LeaveTypes.AnyAsync(lt => lt.Name == name) == false;
        }
    }
}
