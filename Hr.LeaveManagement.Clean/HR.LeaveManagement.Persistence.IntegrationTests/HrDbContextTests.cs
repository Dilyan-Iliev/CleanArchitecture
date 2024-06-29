using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests
{
    public class HrDbContextTests
    {
        private readonly HrDatabaseContext _db;

        public HrDbContextTests()
        {
            //setup mock DB (in-memory DB)
            var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
                .UseInMemoryDatabase("Test-HrDB").Options;

            _db = new HrDatabaseContext(dbOptions);
        }

        [Fact]
        public void Db_Should_Be_Initialized()
        {
            _db.ShouldNotBeNull();
        }

        [Fact]
        public async Task SaveChanges_Should_SetDateCreatedValue()
        {
            //Arrange
            var leaveType = new LeaveType { Id = 1, DefaultDays = 10, Name = "Test" };

            //Act
            await _db.LeaveTypes.AddAsync(leaveType);
            await _db.SaveChangesAsync();

            //Assert
            _db.LeaveTypes.ShouldNotBeEmpty();
            leaveType.DateCreated.ShouldNotBeNull();
        }

        [Fact]
        public async Task SaveChanges_Should_SetDateModifiedValue()
        {
            //Arrange
            var leaveType = new LeaveType { Id = 1, DefaultDays = 10, Name = "Test" };

            //Act
            await _db.LeaveTypes.AddAsync(leaveType);
            await _db.SaveChangesAsync();

            //Assert
            _db.LeaveTypes.ShouldNotBeEmpty();
            leaveType.DateModified.ShouldNotBeNull();
        }
    }
}
