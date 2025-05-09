using Application.Abstractions.Services;
using Application.Commands.JobApplications.Delete;
using Common;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.JobApplication;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("applications/{id:guid}", async (Guid id, IJobApplicationService service, CancellationToken cancellationToken) =>
        {
            var command = new DeleteCommand(id);

            Result result = await service.DeleteAsync(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Applications);
    }
}
