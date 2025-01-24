using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GraduationProject.Entites;

namespace GraduationProject.Models.Config
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skills>
    {
        public void Configure(EntityTypeBuilder<Skills> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1)
                .IsRequired();

            //builder.Property(x => x.TechnicalSkills)
            //    .HasConversion(
            //    tags => string.Join(",", tags),
            //    tags => tags.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList());


            builder.Property(i => i.TechnicalSkills)
                .HasConversion(
                    new ValueConverter<List<string>, string>(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),           // Convert List<string> to JSON string
                        v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>() // Convert JSON string to List<string>
                    ))
                .IsRequired();

           

            //builder.Property(x => x.SoftSkills)
            //    .HasConversion(
            //    tags => string.Join(",", tags),
            //    tags => tags.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList());


            builder.ToTable("Skills");

        }
    }
}
