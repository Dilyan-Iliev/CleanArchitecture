using FluentValidation;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandRequestValidator
        : AbstractValidator<ChangeLeaveRequestApprovalCommandRequest>
    {
        public ChangeLeaveRequestApprovalCommandRequestValidator()
        {
            RuleFor(x => x.Approved)
                .NotNull()
                .WithMessage("Approval status cannot be null");
        }
    }
}
