using Application.Abstractions.Services;
using Application.Commands.JobApplications.Create;
using Common;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.JobApplication;

internal sealed class Create : IEndpoint
{
    public sealed class CreateJobApplicationRequest
    {
        public string Position { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public int StatusId { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/applications", async (CreateJobApplicationRequest request, IJobApplicationService service, CancellationToken cancellationToken) =>
        {
            var command = new CreateCommand(
                request.Position,
                request.CompanyName,
                request.ApplicationDate,
                request.StatusId);

            Result<Guid> result = await service.CreateAsync(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .Accepts<CreateJobApplicationRequest>("application/json")
        .Produces<Guid>(StatusCodes.Status201Created)
        .WithTags(Tags.Applications);
    }
}
