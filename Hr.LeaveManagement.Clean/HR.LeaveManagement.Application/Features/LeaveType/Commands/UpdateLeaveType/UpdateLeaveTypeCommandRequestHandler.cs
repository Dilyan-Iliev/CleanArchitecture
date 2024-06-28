using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandRequestHandler
        : IRequestHandler<UpdateLeaveTypeCommandRequest, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<UpdateLeaveTypeCommandRequestHandler> _logger;

        public UpdateLeaveTypeCommandRequestHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<UpdateLeaveTypeCommandRequestHandler> logger)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }


        public async Task<Unit> Handle(UpdateLeaveTypeCommandRequest request,
            CancellationToken cancellationToken)
        {
            //Validate incoming data
            var validator = new UpdateLeaveTypeCommandRequestValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation errors in update request for {0} - {1}",
                    nameof(LeaveType), request.Id);

                throw new BadRequestException("Invalid LeaveType", validationResult);
            }

            //Convert to domain entity
            var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

            //Perform DB operation
            await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

            //Return result
            return Unit.Value;
        }
    }
}
