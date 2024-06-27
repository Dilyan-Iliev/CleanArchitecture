using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandRequest
        : IRequest<int> //will return the id of the created LeaveType
    {
        //what we need to create LeaveType:
        public string Name { get; set; } = string.Empty;

        public int DefaultDays { get; set; }
    }
}
