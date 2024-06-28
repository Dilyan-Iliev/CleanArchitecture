using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandRequest
        : IRequest<Unit>
    {
        public DeleteLeaveAllocationCommandRequest(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}
