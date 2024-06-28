using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails
{
    public class GetLeaveRequestDetailsQueryRequest
        : IRequest<LeaveRequestDetailsDto>
    {
        public GetLeaveRequestDetailsQueryRequest(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}
