using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandRequestHandler
        : IRequestHandler<CreateLeaveTypeCommandRequest, int>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeCommandRequestHandler(IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<int> Handle(CreateLeaveTypeCommandRequest request,
            CancellationToken cancellationToken)
        {
            //Validate incoming data
            var validator = new CreateleaveTypeCommandRequestValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid LeaveType", validationResult);
            }

            //Convert to domain entity
            var leaveType = _mapper.Map<Domain.LeaveType>(request);

            //Perform DB operation
            await _leaveTypeRepository.CreateAsync(leaveType);

            //Return result
            return leaveType.Id;
        }
    }
}
