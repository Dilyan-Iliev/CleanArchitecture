using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;

namespace HR.LeaveManaegment.Application.UnitTests.Mocks
{
    public class MockLeaveTypeRepo
    {
        public static Mock<ILeaveTypeRepository> GetLeaveTypeMockLeaveTypeRepo()
        {
            var testLeaveTypes = new List<LeaveType>
            {
                new() {Id = 1, DefaultDays = 10, Name = "Test 1"},
                new() {Id = 2, DefaultDays = 15, Name = "Test 2"},
                new() {Id = 3, DefaultDays = 20, Name = "Test 3"},
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();
            mockRepo.Setup(x => x.GetAsync())
                .ReturnsAsync(testLeaveTypes);

            mockRepo.Setup(x => x.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType lt) =>
                {
                    testLeaveTypes.Add(lt);
                    return Task.CompletedTask;
                });

            return mockRepo;
        }
    }
}
