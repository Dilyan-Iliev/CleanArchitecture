using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class GetLeaveRequestListQueryRequest
        : IRequest<IEnumerable<LeaveRequestListDto>>
    {
    }
}
