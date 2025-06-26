using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using CareerGuidancePlatform.Models;
using System.Text.Json;
using CareerGuidancePlatform.Entities;

namespace CareerGuidancePlatform.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        // ✅ Add missing DbSet for Job & ApplicationTracker
        public DbSet<Job> Jobs { get; set; }
        public DbSet<ApplicationTracker> ApplicationTrackers { get; set; }
        public DbSet<EmployerReview> EmployerReviews { get; set; }
        public DbSet<Company> Companies { get; set; }
        // Mentorship Program
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<Resume> Resumes { get; set; }

        public DbSet<MeetingRequest> MeetingRequests { get; set; }
        public DbSet<GroupSession> GroupSessions { get; set; }

        public DbSet<RoadmapStep> RoadmapSteps { get; set; }
        public DbSet<UserRoadmapProgress> UserRoadmapProgresses { get; set; }

        public DbSet<PhaseSubstep> PhaseSubsteps { get; set; }

        public DbSet<UserCalendar> UserCalendars { get; set; }

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

            // **Substep ↔️ Progress**
            modelBuilder.Entity<UserRoadmapProgress>()
            .HasOne(p => p.Substep)
            .WithMany(s => s.UserProgress)
            .HasForeignKey(p => p.StepId);

            // Mapping PhaseSubstep -> RoadmapStep
            modelBuilder.Entity<PhaseSubstep>()
              .HasOne(s => s.RoadmapStep)
              .WithMany(r => r.PhaseSubsteps)     // RoadmapStep có ICollection<PhaseSubstep> PhaseSubsteps
              .HasForeignKey(s => s.RoadmapstepId);

            modelBuilder.Entity<Mentor>()
                .HasOne(m => m.User)
                .WithMany()                // hoặc WithMany(u => u.Mentors) nếu bạn map collection
                .HasForeignKey(m => m.UserId);
        }
    }
}
