using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
    public class GetAllLeaveAllocationsQueryRequest
        : IRequest<IEnumerable<LeaveAllocationDto>>
    {
    }
}
