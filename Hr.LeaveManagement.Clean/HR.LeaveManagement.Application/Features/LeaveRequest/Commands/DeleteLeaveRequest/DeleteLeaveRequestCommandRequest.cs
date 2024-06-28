using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest
{
    public class DeleteLeaveRequestCommandRequest
        : IRequest<Unit>
    {
        public DeleteLeaveRequestCommandRequest(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}
