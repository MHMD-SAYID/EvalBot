
namespace GraduationProject.Entities.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsMany(x => x.RefreshTokens)
                 .ToTable("RefreshTokens")
                 .WithOwner()
                 .HasForeignKey("UserId");
            
        }
    }
}
