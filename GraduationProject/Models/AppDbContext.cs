
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Models
{
    namespace GraduationProject.Models
    {
        public class AppDbContext : IdentityDbContext<ApplicationUser>
        {
            public DbSet<CV> CV { get; set; }
            public DbSet<Interview> Interview { get; set; }
            public DbSet<project> Projects { get; set; }
            public DbSet<Skills> Skills { get; set; }
            public DbSet<Track> Track { get; set; }
            public DbSet<Experience> Experience { get; set; }
            public DbSet<Company> Companies { get; set; }

            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {

            }
            protected override void OnModelCreating(ModelBuilder builder)
            {
                builder.Entity<Interview>()
       .HasOne(i => i.track) // Interview has one Track
       .WithOne(t => t.interview) // Track has one Interview
       .HasForeignKey<Interview>(i => i.TracksId);
                base.OnModelCreating(builder);
            }
        }
    }

}
