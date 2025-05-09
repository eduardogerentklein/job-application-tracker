using Domain.Primitives;

namespace Domain.Entities;

public sealed class JobApplicationStatus : Entity<int>
{
    public required string Name { get; set; }
}
