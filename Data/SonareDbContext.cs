using Microsoft.EntityFrameworkCore;
using Sonare.Models;

namespace Sonare.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Clip> Clips { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Collaboration> Collaborations { get; set; } = null!;
        public DbSet<ClipCollaborator> ClipCollaborators { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite primary key for ClipCollaborator
            modelBuilder.Entity<ClipCollaborator>()
                .HasKey(cc => new { cc.ClipId, cc.UserId });

            // Clip -> User
            modelBuilder.Entity<Clip>()
                .HasOne(c => c.User)
                .WithMany(u => u.Clips)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Comment -> Clip
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Clip)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.ClipId)
                .OnDelete(DeleteBehavior.Cascade);

            // Comment -> User
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Collaboration -> OriginalClip
            modelBuilder.Entity<Collaboration>()
                .HasOne(c => c.OriginalClip)
                .WithMany(c => c.OriginalCollaborations)
                .HasForeignKey(c => c.OriginalClipId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading circular delete

            // Collaboration -> ResponseClip
            modelBuilder.Entity<Collaboration>()
                .HasOne(c => c.ResponseClip)
                .WithMany(c => c.ResponseCollaborations)
                .HasForeignKey(c => c.ResponseClipId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading circular delete

            // ClipCollaborator -> Clip
            modelBuilder.Entity<ClipCollaborator>()
                .HasOne(cc => cc.Clip)
                .WithMany(c => c.ClipCollaborators)
                .HasForeignKey(cc => cc.ClipId)
                .OnDelete(DeleteBehavior.Cascade);

            // ClipCollaborator -> User
            modelBuilder.Entity<ClipCollaborator>()
                .HasOne(cc => cc.User)
                .WithMany(u => u.ClipCollaborations)
                .HasForeignKey(cc => cc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data
            modelBuilder.Entity<User>().HasData(UserData.Users);
            modelBuilder.Entity<Clip>().HasData(ClipData.Clips);
            modelBuilder.Entity<ClipCollaborator>().HasData(ClipCollaboratorData.ClipCollaborators);
            modelBuilder.Entity<Collaboration>().HasData(CollaborationData.Collaborations);
            modelBuilder.Entity<Comment>().HasData(CommentData.Comments);
        }
    }
}
