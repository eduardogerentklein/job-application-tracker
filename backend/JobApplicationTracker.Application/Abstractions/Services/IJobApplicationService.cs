using Application.Commands.JobApplications.Create;
using Application.Commands.JobApplications.Delete;
using Application.Commands.JobApplications.Update;
using Application.Queries.JobApplications;
using Application.Queries.JobApplications.GetById;
using Common;

namespace Application.Abstractions.Services
{
    public interface IJobApplicationService : IServiceBase
    {
        Task<Result<Guid>> CreateAsync(CreateCommand request, CancellationToken cancellationToken);
        Task<Result> UpdateAsync(UpdateCommand command, CancellationToken cancellationToken);
        Task<Result> DeleteAsync(DeleteCommand command, CancellationToken cancellationToken);
        Task<Result<GetResponse>> GetByIdAsync(GetByIdQuery query, CancellationToken cancellationToken);
        Task<Result<IEnumerable<GetResponse>>> GetAsync(CancellationToken cancellationToken);
        Task<Result<GetPagedResponse>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
