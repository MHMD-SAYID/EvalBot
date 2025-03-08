using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraduationProject.Models.Config
{
    public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1)
                .IsRequired();

            builder.Property(x => x.CompanyName)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();
            

            builder.ToTable("Experience");

        }
    }
}
