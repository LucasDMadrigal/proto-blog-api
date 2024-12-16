using Microsoft.EntityFrameworkCore;

namespace proto_blog_api.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext( DbContextOptions<StoreContext> options ) : base( options ) { }

        /*
         representamos las entidades en la base de datos
         */
        public DbSet<User> Users { get; set; }
        public DbSet<Publications> Publications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar relación M:N entre Authors y Publications
            modelBuilder.Entity<Publications>()
                .HasMany(p => p.Authors)
                .WithMany(a => a.Publications)
                .UsingEntity(j => j.ToTable("AuthorPublications"));
        }
    }
}
