using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoadStoryTracking.WebApi.Data.Models;

namespace RoadStoryTracking.WebApi.Data.Context
{
    public class RoadStoryTrackingDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Friend> Friends { get; set; }

        public DbSet<MarkerImage> MarkerImages { get; set; }

        public DbSet<Marker> Markers { get; set; }

        public RoadStoryTrackingDbContext(DbContextOptions<RoadStoryTrackingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("User", "dbo");
            modelBuilder.Entity<IdentityRole>().ToTable("Role", "dbo");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "dbo");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "dbo");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "dbo");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "dbo");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "dbo");

            modelBuilder.Entity<Friend>()
                .HasIndex(f => new { f.RequestedById, f.RequestedToId })
                .IsUnique();

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.RequestedBy)
                .WithMany(u => u.Friends);
        }
    }
}