using Domain.Primitives;

namespace Domain.Entities;

public sealed class JobApplication : Entity<Guid>
{
    public required string Position { get; set; }
    public required string CompanyName { get; set; }
    public DateTime ApplicationDate { get; set; }
    public int StatusId { get; set; }
}
