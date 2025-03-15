namespace GraduationProject.Entities.Config
{
    public class BusinessAccountConfiguration : IEntityTypeConfiguration<BusinessAccount>
    {
        public void Configure(EntityTypeBuilder<BusinessAccount> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);
            builder.Property(x => x.Type)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.HasOne(x => x.User)
                .WithMany(x => x.businessAccounts)
                .HasForeignKey(x => x.UserId);


            builder.ToTable("BusinessAccount");

        }


    }
}
