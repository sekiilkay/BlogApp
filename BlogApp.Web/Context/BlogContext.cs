using BlogApp.Web.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BlogApp.Web.Context
{
    public class BlogContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Post silinirse ilgili postun yorumları silinsin
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Kullanıcı silinirse yorumları silinmesin (Post <-> Comment)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.AppUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Kullanıcı silinirse kullanıcının postları silinsin
            modelBuilder.Entity<Post>()
                .HasOne(p => p.AppUser)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
