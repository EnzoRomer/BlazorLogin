using Microsoft.EntityFrameworkCore;
using BlazorLogin.Shared;

namespace BlazorLogin.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        // Esto le dice a Entity Framework que use el nombre exacto de la tabla
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("usuario");
        }
    }
}