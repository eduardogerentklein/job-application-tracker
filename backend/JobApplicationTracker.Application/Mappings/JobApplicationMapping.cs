using Application.Queries.JobApplications;
using Domain.Entities;

namespace Application.Mappings
{
    public static class JobApplicationMapping
    {
        public static GetResponse ToResponse(this JobApplication app) => 
            new(
                app.Id, 
                app.Position, 
                app.CompanyName, 
                app.ApplicationDate, 
                app.StatusId);
    }
}
