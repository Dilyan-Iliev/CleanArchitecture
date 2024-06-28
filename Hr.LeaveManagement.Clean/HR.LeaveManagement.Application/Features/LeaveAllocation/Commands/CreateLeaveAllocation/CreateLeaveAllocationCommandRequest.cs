using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandRequest
        : IRequest<Unit>
    {
        public int LeaveTypeId { get; set; }
    }
}
