namespace GraduationProject.Models.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {


        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.OwnsMany(x=>x.RefreshTokens)
                .ToTable("RefreshTokens")
                .WithOwner()
                .HasForeignKey("UserId");


        }
    }
}
