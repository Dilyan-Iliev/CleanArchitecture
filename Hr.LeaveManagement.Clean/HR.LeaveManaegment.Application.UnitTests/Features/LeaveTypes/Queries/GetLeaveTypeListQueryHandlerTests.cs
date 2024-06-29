using AutoMapper;
using HR.LeaveManaegment.Application.UnitTests.Mocks;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using Moq;
using Shouldly;

namespace HR.LeaveManaegment.Application.UnitTests.Features.LeaveTypes.Queries
{
    public class GetLeaveTypeListQueryHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<IAppLogger<GetLeaveTypesQueryRequestHandler>> _mockLogger;

        public GetLeaveTypeListQueryHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepo.GetLeaveTypeMockLeaveTypeRepo();

            var mapperConfig = new MapperConfiguration(mapper =>
            {
                mapper.AddProfile<LeaveTypeProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _mockLogger = new Mock<IAppLogger<GetLeaveTypesQueryRequestHandler>>();
        }

        [Fact]
        public async Task GetLeaveTypeListTest()
        {
            var handler = new GetLeaveTypesQueryRequestHandler(_mapper,
                _mockRepo.Object, _mockLogger.Object);

            var result = await handler.Handle(new GetLeaveTypesQueryRequest(), CancellationToken.None);

            result.ShouldNotBe(null);
            result.ShouldBeOfType<List<LeaveTypeDto>>();
            result.Count().ShouldBe(3);
        }
    }
}
