using Domain.Enums;
using FluentValidation;

namespace Application.Commands.JobApplications.Create
{
    internal sealed class CreateCommandValidator : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidator()
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
                .WithMessage("Application date must be a valid date.");

            RuleFor(c => c.Status)
                .IsInEnum();
        }
    }
}
