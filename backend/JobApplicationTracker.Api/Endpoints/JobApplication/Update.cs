using Application.Abstractions.Services;
using Application.Commands.JobApplications.Update;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.JobApplication;

internal sealed class Update : IEndpoint
{
    public sealed class UpdateJobApplicationRequest
    {
        public string Position { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public DateTime? ApplicationDate { get; set; }
        public int StatusId { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("applications/{id:guid}", async (Guid id, UpdateJobApplicationRequest request, IJobApplicationService service, CancellationToken cancellationToken) =>
        {
            var command = new UpdateCommand(
                id,
                request.Position,
                request.CompanyName,
                request.ApplicationDate,
                request.StatusId);

            var result = await service.UpdateAsync(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Applications);
    }
}