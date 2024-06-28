using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class GetLeaveTypeDetailsQueryRequest
        : IRequest<LeaveTypeDetailsDto>
    {
        public GetLeaveTypeDetailsQueryRequest(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}