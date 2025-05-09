using FluentValidation;

namespace Application.Commands.JobApplications.Update
{
    internal sealed class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator() 
        {
            RuleFor(c => c.Position)
                .NotEmpty()
                .WithMessage("Position is required.")
                .MaximumLength(100)
                .WithMessage("Position must not exceed 100 characters.");

            RuleFor(c => c.CompanyName)
                .NotEmpty()
                .WithMessage("Company name is required.")
                .MaximumLength(100)
                .WithMessage("Company name must not exceed 100 characters.");

            RuleFor(c => c.ApplicationDate)
                .Must(date => date != default)
                .When(x => x.ApplicationDate.HasValue)
                .WithMessage("Application date must be a valid date.");

            RuleFor(c => c.Status)
                .IsInEnum();
        }
    }
}
