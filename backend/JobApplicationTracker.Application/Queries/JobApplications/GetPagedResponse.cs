namespace Application.Queries.JobApplications;

public record GetPagedResponse(
    List<GetResponse> Items,
    int PageNumber,
    int PageSize,
    int TotalCount);