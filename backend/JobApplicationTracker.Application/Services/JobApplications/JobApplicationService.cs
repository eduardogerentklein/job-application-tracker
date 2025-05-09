using Application.Abstractions.Services;
using Application.Commands.JobApplications.Create;
using Application.Commands.JobApplications.Delete;
using Application.Commands.JobApplications.Update;
using Application.Mappings;
using Application.Queries.JobApplications;
using Application.Queries.JobApplications.GetById;
using Common;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services.JobApplications
{
    public class JobApplicationService(
        IJobApplicationRepository repository,
        IDateTimeProvider dateTimeProvider,
        ILogger<JobApplicationService> logger,
        IValidator<CreateCommand> createValidator,
        IValidator<UpdateCommand> updateValidator,
        IValidator<DeleteCommand> deleteValidator) : IJobApplicationService
    {
        private readonly IJobApplicationRepository _repository = repository;
        private readonly IValidator<CreateCommand> _createValidator = createValidator;
        private readonly IValidator<UpdateCommand> _updateValidator = updateValidator;
        private readonly IValidator<DeleteCommand> _deleteValidator = deleteValidator;
        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

        private readonly ILogger<JobApplicationService> _logger = logger;

        public async Task<Result<Guid>> CreateAsync(CreateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing request {RequestName}", nameof(CreateAsync));

            var validationResult = await _createValidator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Completed command {CommandName} with errors: {Errors}", nameof(CreateAsync), validationResult.Errors);
                return Result.Failure<Guid>(JobApplicationErrors.CreateValidationFailed());
            }

            var jobApplication = new JobApplication
            {
                Id = Guid.NewGuid(),
                Position = request.Position,
                CompanyName = request.CompanyName,
                ApplicationDate = request.ApplicationDate ?? _dateTimeProvider.UtcNow,
                StatusId = (int)request.Status
            };

            await _repository.AddAsync(jobApplication, cancellationToken);
            _logger.LogInformation("Completed command {CommandName}", nameof(CreateAsync));

            return Result.Success(jobApplication.Id);
        }

        public async Task<Result> UpdateAsync(UpdateCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing request {RequestName}", nameof(UpdateAsync));

            var validationResult = await _updateValidator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Completed command {CommandName} with error", nameof(UpdateAsync));
                return Result.Failure(JobApplicationErrors.CreateValidationFailed());
            }

            var jobApplication = await _repository.GetByIdAsync(command.Id, cancellationToken);

            if (jobApplication is null) return Result.Failure(JobApplicationErrors.NotFound(command.Id));

            var updatedJobApplication = new JobApplication()
            {
                Id = command.Id,
                Position = command.Position,
                CompanyName = command.CompanyName,
                ApplicationDate = command.ApplicationDate ?? _dateTimeProvider.UtcNow,
                StatusId = (int)command.Status
            };

            await _repository.UpdateAsync(jobApplication, updatedJobApplication, cancellationToken);
            _logger.LogInformation("Completed command {CommandName}", nameof(UpdateAsync));

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(DeleteCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing request {RequestName}", nameof(DeleteAsync));

            var validationResult = await _deleteValidator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogError("Completed command {CommandName} with errors", nameof(DeleteAsync));
                return Result.Failure(JobApplicationErrors.CreateValidationFailed());
            }

            var jobApplication = await _repository.GetByIdAsync(command.Id, cancellationToken);

            if (jobApplication is null) return Result.Failure(JobApplicationErrors.NotFound(command.Id));

            await _repository.DeleteAsync(jobApplication, cancellationToken);

            _logger.LogInformation("Completed command {CommandName}", nameof(DeleteAsync));

            return Result.Success();
        }

        public async Task<Result<GetResponse>> GetByIdAsync(GetByIdQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing request {RequestName}", nameof(GetByIdAsync));

            var jobApplication = await _repository.GetByIdAsync(query.Id, cancellationToken);

            if (jobApplication is null) return Result.Failure<GetResponse>(JobApplicationErrors.NotFound(query.Id));

            _logger.LogInformation("Completed query {QueryName}", nameof(GetByIdAsync));

            var response = jobApplication.ToResponse();

            return Result.Success(response);
        }

        public async Task<Result<IEnumerable<GetResponse>>> GetAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing request {RequestName}", nameof(GetAsync));

            var jobApplications = await _repository.GetAllAsync(cancellationToken);

            var response = jobApplications
                .Select(app => app.ToResponse())
                .ToList();

            _logger.LogInformation("Completed query {QueryName}", nameof(GetAsync));

            return Result.Success<IEnumerable<GetResponse>>(response);
        }

        public async Task<Result<GetPagedResponse>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing request {RequestName}", nameof(GetPagedAsync));

            var jobApplications = await _repository.GetAllAsync(cancellationToken);


            var paged = jobApplications
                .Skip((pageNumber -1 ) * pageSize)
                .Take(pageSize)
                .Select(app => app.ToResponse())
                .ToList();

            var response = new GetPagedResponse(
                    paged,
                    pageNumber,
                    pageSize,
                    jobApplications.Count);

            _logger.LogInformation("Completed query {QueryName}", nameof(GetPagedAsync));

            return Result.Success(response);
        }
    }
}
