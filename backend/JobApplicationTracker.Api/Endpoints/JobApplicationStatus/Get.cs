using Application.Abstractions.Services;
using Application.Queries.ApplicationStatus.Get;
using Common;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.JobApplicationStatus;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("status", async (IApplicationStatusService service, CancellationToken cancellationToken) =>
        {
            Result<IEnumerable<GetApplicationStatusResponse>> result = await service.GetAsync(cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.ApplicationStatus);
    }
}