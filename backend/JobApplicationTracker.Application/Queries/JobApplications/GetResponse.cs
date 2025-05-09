namespace Application.Queries.JobApplications;

public record GetResponse(
    Guid Id,
    string Position,
    string CompanyName,
    DateTime ApplicationDate,
    int StatusId
);