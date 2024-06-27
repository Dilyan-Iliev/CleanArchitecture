using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class GetLeaveTypeDetailsQueryRequest
        : IRequest<LeaveTypeDetailsDto>
    {
        public int Id { get; set; }
    }
}