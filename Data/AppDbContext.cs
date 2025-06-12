    using Microsoft.EntityFrameworkCore;
    using CareerGuidancePlatform.Models;

    namespace CareerGuidancePlatform.Data
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<User> Users { get; set; }

            // ✅ Thêm các bảng cho Mentorship Program
            public DbSet<Mentor> Mentors { get; set; }
            public DbSet<Message> Messages { get; set; }
            public DbSet<MeetingRequest> MeetingRequests { get; set; }
            public DbSet<GroupSession> GroupSessions { get; set; }

            public DbSet<RoadmapStep> RoadmapSteps { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<RoadmapStep>()
                    .Property(r => r.ResourceLinks)
                    .HasConversion(
                        v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions)null!),
                        v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v) ?? new List<string>());
            }
        }
    }
