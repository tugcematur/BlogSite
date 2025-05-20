using BlogApp.Models.Data.Classes;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Models.Data
{
    public class blogContext :DbContext
    {
        public blogContext(DbContextOptions<blogContext> options) : base(options) { }


       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comments>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Kullanıcı silinirse, yorumu silme

            modelBuilder.Entity<Comments>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Gönderi silinirse, yorumları da sil

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>()
          .HasIndex(u => u.Email)
          .IsUnique();
        }

        public DbSet<Users> User { get; set; }
        public DbSet<Categories> Category { get; set; }
        public DbSet<Posts> Post { get; set; }
        public DbSet<Comments> Comment { get; set; }
      

    }
}
