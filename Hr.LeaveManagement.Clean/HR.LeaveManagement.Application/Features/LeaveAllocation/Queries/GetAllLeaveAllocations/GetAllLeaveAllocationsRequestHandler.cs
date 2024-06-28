using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
    public class GetAllLeaveAllocationsRequestHandler
        : IRequestHandler<GetAllLeaveAllocationsQueryRequest, IEnumerable<LeaveAllocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public GetAllLeaveAllocationsRequestHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<IEnumerable<LeaveAllocationDto>> Handle(GetAllLeaveAllocationsQueryRequest request,
            CancellationToken cancellationToken)
        {
            var leaveAllocations = await _leaveAllocationRepository.GetAsync();

            var data = _mapper.Map<IEnumerable<LeaveAllocationDto>>(leaveAllocations);
            return data;
        }
    }
}
