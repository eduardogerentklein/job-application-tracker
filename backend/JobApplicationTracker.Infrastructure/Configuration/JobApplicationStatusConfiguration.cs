using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal class JobApplicationStatusConfiguration : IEntityTypeConfiguration<JobApplicationStatus>
{
    public void Configure(EntityTypeBuilder<JobApplicationStatus> builder)
    {
        builder.HasKey(app => app.Id);

        builder.Property(app => app.Name)
            .IsRequired()
            .HasMaxLength(50);

        SeedData(builder);
    }

    private static void SeedData(EntityTypeBuilder<JobApplicationStatus> builder)
    {
        builder.HasData(
            new JobApplicationStatus { Id = 1, Name = ApplicationStatus.Applied.ToString() },
            new JobApplicationStatus { Id = 2, Name = ApplicationStatus.Interview.ToString() },
            new JobApplicationStatus { Id = 3, Name = ApplicationStatus.Offer.ToString() },
            new JobApplicationStatus { Id = 4, Name = ApplicationStatus.Accepted.ToString() },
            new JobApplicationStatus { Id = 5, Name = ApplicationStatus.Rejected.ToString() }
        );
    }
}
