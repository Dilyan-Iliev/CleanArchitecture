using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandRequest
        : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
