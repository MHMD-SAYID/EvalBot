using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraduationProject.Models.Config
{
    public class projectConfiguration : IEntityTypeConfiguration<project>
    {
        public void Configure(EntityTypeBuilder<project> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(x => x.Description)
                .HasColumnType("varchar")
                .IsRequired();

            builder.Property(x => x.GitHupLink)
                .HasColumnType("varchar")
                .IsRequired();

            builder.Property(x => x.StartDate)
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder.Property(x => x.EndDate)
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder.ToTable("Project");
        }
    }
}
