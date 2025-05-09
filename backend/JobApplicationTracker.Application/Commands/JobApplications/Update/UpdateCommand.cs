using Domain.Enums;

namespace Application.Commands.JobApplications.Update
{
    public record UpdateCommand(
        Guid Id,
        string Position,
        string CompanyName,
        DateTime? ApplicationDate,
        ApplicationStatus Status
    );
}
