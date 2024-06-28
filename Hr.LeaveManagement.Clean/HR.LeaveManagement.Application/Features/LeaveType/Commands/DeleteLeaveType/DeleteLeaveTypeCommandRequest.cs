using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandRequest
        : IRequest<Unit>
    {
        public DeleteLeaveTypeCommandRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

    }
}
