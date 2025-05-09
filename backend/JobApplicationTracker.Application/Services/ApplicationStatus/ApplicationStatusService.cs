using Application.Abstractions.Services;
using Application.Mappings;
using Application.Queries.ApplicationStatus.Get;
using Common;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services.ApplicationStatus
{
    public class ApplicationStatusService(
        IApplicationStatusRepository repository,
        ILogger<ApplicationStatusService> logger) : IApplicationStatusService
    {
        private readonly IApplicationStatusRepository _repository = repository;

        private readonly ILogger<ApplicationStatusService> _logger = logger;

        public async Task<Result<IEnumerable<GetApplicationStatusResponse>>> GetAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing request {RequestName} in {Service}", nameof(GetAsync), nameof(ApplicationStatusService));

            var statuses = await _repository.GetAllAsync(cancellationToken);

            var response = statuses
                .Select(entity => entity.ToResponse())
                .ToList();

            _logger.LogInformation("Completed query {QueryName} in {Service}", nameof(GetAsync), nameof(ApplicationStatusService));

            return Result.Success<IEnumerable<GetApplicationStatusResponse>>(response);
        }
    }
}
