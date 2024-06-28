using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandRequestHandler
        : IRequestHandler<CancelLeaveRequestCommandRequest, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;
        private readonly IAppLogger<CancelLeaveRequestCommandRequestHandler> _logger;

        public CancelLeaveRequestCommandRequestHandler(ILeaveRequestRepository leaveRequestRepository,
            IEmailSender emailSender,
            IAppLogger<CancelLeaveRequestCommandRequestHandler> logger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task<Unit> Handle(CancelLeaveRequestCommandRequest request,
            CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null)
            {
                throw new NotFoundException(nameof(LeaveRequest), request.Id);
            }

            leaveRequest.Cancelled = true;

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
