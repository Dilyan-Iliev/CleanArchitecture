using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailsQueryRequest
        : IRequest<LeaveAllocationDetailsDto>
    {
        public GetLeaveAllocationDetailsQueryRequest(int id)
        {
            this.Id = id;   
        }

        public int Id { get; set; }
    }
}
