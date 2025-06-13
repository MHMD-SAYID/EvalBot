
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Text.Json;

namespace GraduationProject.Entities
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public DbSet<Interview> Interview { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Track> Track { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<CompanyProfile> Company { get; set; }
        public DbSet<OtpEntry> OtpEntries { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<UploadedFiles> Files { get; set; }
        public DbSet<BusinessAccount> businessAccounts { get; set; }
        public DbSet<UserCV> UserCV { get; set; }
        public DbSet<UserImage> UserImage { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<CompanyProfile> CompanyProfile { get; set; }
        public DbSet<JobUserProfile> JobUserProfiles { get; set; }
        public DbSet<SoftSkills> SoftSkills { get; set; }
        public DbSet<Q_A> Q_A { get; set; }
        


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            builder.Entity<IdentityUserLogin<string>>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });

            builder.Entity<IdentityUserRole<string>>()
                .HasKey(r => new { r.UserId, r.RoleId });

            builder.Entity<IdentityUserToken<string>>()
                .HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            builder.Entity<User>()
           .HasOne(u => u.userProfile)
           .WithOne(p => p.user)
           .HasForeignKey<UserProfile>(p => p.userId)
           .OnDelete(DeleteBehavior.Cascade);

            // CompanyProfile <-> User one-to-one
            builder.Entity<User>()
                .HasOne(u => u.companyProfile)
                .WithOne(c => c.user)
                .HasForeignKey<CompanyProfile>(c => c.userId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserProfile>()
            .Property(u => u.Skills)
            .HasConversion(
           v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
           v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>());
             builder.Entity<UserProfile>()
        .HasOne(up => up.user)
        .WithOne(u => u.userProfile)
        .HasForeignKey<UserProfile>(up => up.userId)
        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
