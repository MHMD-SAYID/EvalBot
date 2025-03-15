using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GraduationProject.Entities.Config
{
    public class OtpEntryConfiguration : IEntityTypeConfiguration<OtpEntry>
    {


        public void Configure(EntityTypeBuilder<OtpEntry> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1)
                .IsRequired();


            builder.ToTable("OtpEntries");


        }
    }
}
