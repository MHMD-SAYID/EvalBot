
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GraduationProject.Entities.Config
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {


        //public void Configure(EntityTypeBuilder<UserProfile> builder)
        //{
        //    builder.OwnsMany(x => x.RefreshTokens)
        //        .ToTable("RefreshTokens")
        //        .WithOwner()
        //        .HasForeignKey("UserId");
        //    builder.Property(x => x.Bio)
        //        .HasMaxLength(500)
        //        .IsRequired(false);

        //    builder.Property(x => x.Skills)
        //    .HasConversion(
        //        v => string.Join(',', v),
        //        v => v.Split(',', StringSplitOptions.None).ToList())
        //    .Metadata.SetValueComparer(
        //        new ValueComparer<List<string>>(
        //            (c1, c2) => c1.SequenceEqual(c2),
        //            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
        //            c => c.ToList()
        //            ));
        //}
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(x=>x.userId);
            builder.Property(x => x.Bio)
                .HasMaxLength(500)
                .IsRequired(false);
            builder.Property(x => x.Skills)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.None).ToList())
            .Metadata.SetValueComparer(
                new ValueComparer<List<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                    ));
            builder.HasOne(x => x.user)
        .WithOne(u => u.userProfile)
        .HasForeignKey<UserProfile>(x => x.userId)
        .OnDelete(DeleteBehavior.Cascade);

        }
    }
}