using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class GetLeaveRequestListQueryRequestHandler
        : IRequestHandler<GetLeaveRequestListQueryRequest, IEnumerable<LeaveRequestListDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public GetLeaveRequestListQueryRequestHandler(IMapper mapper,
            ILeaveRequestRepository leaveRequestRepository)
        {
            _mapper = mapper;
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<IEnumerable<LeaveRequestListDto>> Handle(GetLeaveRequestListQueryRequest request,
            CancellationToken cancellationToken)
        {
            //check if it is logged in employee

            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
            var data = _mapper.Map<IEnumerable<LeaveRequestListDto>>(leaveRequests);

            //fill requests with employee information

            return data;
        }
    }
}
