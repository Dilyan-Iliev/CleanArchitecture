using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypesQueryRequest
        : IRequest<IEnumerable<LeaveTypeDto>> //what i am expecting to return the request
    {
    }

    //it could be a record instead of class
    //public record GetLeaveTypesQueryRequest: IRequest<IEnumerable<LeaveTypeDto>>;
}
