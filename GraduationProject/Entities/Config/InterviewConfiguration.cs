using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraduationProject.Entities.Config
{
    internal class InterviewConfiguration : IEntityTypeConfiguration<Interview>
    {
        public void Configure(EntityTypeBuilder<Interview> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1)
                .IsRequired();

            //builder.Property(x => x.StartDate)
            //    .HasColumnType("datetime2");

            //builder.Property(x => x.EndDate)
            //    .HasColumnType("datetime2");

            //builder.Property(x => x.FeedBack)
            //    .HasColumnType("varchar");

            //builder.Property(x => x.Score)
            //    .IsRequired();

            //builder.Property(x => x.TracksId);

            //builder.HasOne(x => x.userProfile)
            //    .WithMany(x => x.interviews)
            //    .HasForeignKey(x => x.userProfileId);

            //builder.HasOne(x => x.track)
            //    .WithOne(x => x.interview)
            //    .HasForeignKey<Interview>(x => x.TracksId);
            //builder.HasOne(x=>x.track)
            //    .WithOne(x=>x.interview)
            //    .HasForeignKey<Interview>(x => x.TracksId);

            //builder.Property(i => i.level)
            //    .HasConversion(x=>x.ToString(),x=>Enum.Parse<Level>(x));

            builder.HasMany(x => x.q_a)
                .WithOne(x => x.Interview)
                .HasForeignKey(x => x.InterviewId);
            builder.ToTable("Interview");
                

        }
    }
}
