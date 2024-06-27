using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandRequestHandler
        : IRequestHandler<UpdateLeaveTypeCommandRequest, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeCommandRequestHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }


        public async Task<Unit> Handle(UpdateLeaveTypeCommandRequest request,
            CancellationToken cancellationToken)
        {
            //Validate incoming data - TODO

            //Convert to domain entity
            var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

            //Perform DB operation
            await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

            //Return result
            return Unit.Value;
        }
    }
}
