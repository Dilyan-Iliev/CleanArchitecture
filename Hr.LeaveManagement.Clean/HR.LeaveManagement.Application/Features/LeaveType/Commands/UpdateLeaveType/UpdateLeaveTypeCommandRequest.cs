using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandRequest
        : IRequest<Unit> //returns void -> Unit = void in MediatR
    {
        public string Name { get; set; } = string.Empty;

        public int DefaultDays { get; set; }
    }
}
