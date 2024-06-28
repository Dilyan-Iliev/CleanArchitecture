using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandRequestHandler
        : IRequestHandler<CreateLeaveRequestCommandRequest, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<UpdateLeaveRequestCommandRequestHandler> _logger;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveRequestCommandRequestHandler(IMapper mapper,
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IEmailSender emailSender,
            IAppLogger<UpdateLeaveRequestCommandRequestHandler> logger)
        {
            _mapper = mapper;
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateLeaveRequestCommandRequest request, 
            CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandRequestValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Request", validationResult);
            }

            //Get requestin employee's id

            //Check on employee's allocation

            //if allocations arent enough, return validation error with message

            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
            await _leaveRequestRepository.CreateAsync(leaveRequest);

            try
            {
                //var email = new EmailMessage
                //{
                //    To = string.Empty,
                //    Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                //    $"has been updated successfully.",
                //    Subject = "Leave Request Submitted"
                //};

                //await _emailSender.SendEmail(email
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }

            return Unit.Value;
        }
    }
}
