using Application.Queries.ApplicationStatus.Get;
using Domain.Entities;

namespace Application.Mappings
{
    public static class ApplicationStatusMapping
    {
        public static GetApplicationStatusResponse ToResponse(this JobApplicationStatus status) =>
            new(status.Id, status.Name);
    }
}
