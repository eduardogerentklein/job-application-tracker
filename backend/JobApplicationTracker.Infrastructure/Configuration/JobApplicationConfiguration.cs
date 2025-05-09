using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

internal class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        builder
            .HasKey(app => app.Id);

        builder
            .HasOne<JobApplicationStatus>()
            .WithMany()
            .HasForeignKey(app => app.StatusId);

        builder
            .Property(app => app.Position)
                .IsRequired()
                .HasMaxLength(100);

        builder
            .Property(app => app.CompanyName)
                .IsRequired()
                .HasMaxLength(100);
    }
}
