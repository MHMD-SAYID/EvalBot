using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProject.Models.Config
{
    public class JobConfiguration: IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Users)
                .WithMany(x => x.Jobs);
                
        }

        
    }
}
