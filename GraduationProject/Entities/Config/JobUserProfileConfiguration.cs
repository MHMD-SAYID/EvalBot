
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Entities.Config;

public class JobUserProfileConfiguration : IEntityTypeConfiguration<JobUserProfile>
{
    public void Configure(EntityTypeBuilder<JobUserProfile> builder)
    {


        builder.HasKey(jup => new { jup.jobId, jup.userProfileId });

        builder.HasOne(jup => jup.job)
               .WithMany(j => j.jobUserProfiles)
               .HasForeignKey(jup => jup.jobId);

        builder.HasOne(jup => jup.userProfile)
               .WithMany(up => up.jobUserProfiles)
               .HasForeignKey(jup => jup.userProfileId);
        builder.ToTable(nameof(JobUserProfile));
    }
}

