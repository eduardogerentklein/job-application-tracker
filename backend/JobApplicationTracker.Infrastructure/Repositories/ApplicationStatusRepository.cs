using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
    internal sealed class ApplicationStatusRepository : Repository<JobApplicationStatus, int>, IApplicationStatusRepository
    {
        public ApplicationStatusRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
