using Domain.Enums;

namespace Application.Commands.JobApplications.Create
{
    public record CreateCommand(
        string Position,
        string CompanyName,
        DateTime? ApplicationDate,
        ApplicationStatus Status
    );
}
