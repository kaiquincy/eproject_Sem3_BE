using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using CareerGuidancePlatform.Models;
using System.Text.Json;

namespace CareerGuidancePlatform.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        // Mentorship Program
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MeetingRequest> MeetingRequests { get; set; }
        public DbSet<GroupSession> GroupSessions { get; set; }

        public DbSet<RoadmapStep> RoadmapSteps { get; set; }
        public DbSet<UserRoadmapProgress> UserRoadmapProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 2) Composite PK và relationship cho UserRoadmapProgress
            modelBuilder.Entity<UserRoadmapProgress>()
                .HasKey(p => new { p.UserId, p.StepId });

            modelBuilder.Entity<UserRoadmapProgress>()
                .HasOne(p => p.User)
                .WithMany(u => u.RoadmapProgresses)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<UserRoadmapProgress>()
                .HasOne(p => p.RoadmapStep)
                .WithMany()
                .HasForeignKey(p => p.StepId);
        }
    }
}
