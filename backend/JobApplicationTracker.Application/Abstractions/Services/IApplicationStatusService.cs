using Application.Queries.ApplicationStatus.Get;
using Common;

namespace Application.Abstractions.Services
{
    public interface IApplicationStatusService : IServiceBase
    {
        Task<Result<IEnumerable<GetApplicationStatusResponse>>> GetAsync(CancellationToken cancellationToken);
    }
}
