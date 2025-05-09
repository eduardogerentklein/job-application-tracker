using Application.Abstractions.Services;
using Application.Queries.JobApplications;
using Application.Queries.JobApplications.GetById;
using Common;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.JobApplication;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("applications/{id:guid}", async (Guid id, IJobApplicationService service, CancellationToken cancellationToken) =>
        {
            var query = new GetByIdQuery(id);

            Result<GetResponse> result = await service.GetByIdAsync(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Applications);
    }
}
