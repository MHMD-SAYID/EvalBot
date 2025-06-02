using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraduationProject.Entities.Config
{
    public class CompanyProfileConfiguration : IEntityTypeConfiguration<CompanyProfile>
    {


        public void Configure(EntityTypeBuilder<CompanyProfile> builder)
        {
            //builder.HasKey(x => x.userIdId);
            //builder.Property(x => x.Id)
            //    .UseIdentityColumn(1, 1)
            //    .IsRequired();
            builder.HasKey(x => x.userId);

            builder.HasOne(x => x.user)
        .WithOne(u => u.companyProfile)
        .HasForeignKey<UserProfile>(x => x.userId)
        .OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("companyProfile");


        }
    }
}
