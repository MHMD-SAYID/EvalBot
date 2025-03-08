
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GraduationProject.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public DbSet<Interview> Interview { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Track> Track { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<OtpEntry> OtpEntries { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<BusinessAccount> businessAccounts { get; set; }


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

            builder.Entity<User>()
            .Property(u => u.Skills)
            .HasConversion(
           v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
           v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>());
        }
    }
}
