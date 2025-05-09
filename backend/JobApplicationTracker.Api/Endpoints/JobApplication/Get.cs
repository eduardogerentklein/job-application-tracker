using Application.Abstractions.Services;
using Application.Queries.JobApplications;
using Common;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.JobApplication;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("applications", async (
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            IJobApplicationService service, 
            CancellationToken cancellationToken) =>
        {
            Result<GetPagedResponse> result = await service.GetPagedAsync(pageNumber, pageSize, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Applications);
    }
}
