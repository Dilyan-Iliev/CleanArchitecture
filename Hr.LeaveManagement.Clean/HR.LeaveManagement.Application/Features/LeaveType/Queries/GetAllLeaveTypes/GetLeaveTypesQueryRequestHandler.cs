using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypesQueryRequestHandler
        : IRequestHandler<GetLeaveTypesQueryRequest, IEnumerable<LeaveTypeDto>>
    //request type and returned result
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypesQueryRequestHandler> _logger;

        public GetLeaveTypesQueryRequestHandler(IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository,
            IAppLogger<GetLeaveTypesQueryRequestHandler> logger)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<LeaveTypeDto>> Handle(GetLeaveTypesQueryRequest request,
            CancellationToken cancellationToken)
        {
            //Query the DB
            var leaveTypes = await _leaveTypeRepository.GetAsync();

            //Conveert data objects to DTO objects
            var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            //return IEnumerable of DTO objects
            _logger.LogInformation("Leave types were retrieved successfully");
            return data;
        }
    }
}
