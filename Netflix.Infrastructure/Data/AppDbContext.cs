using Microsoft.EntityFrameworkCore;
using Netflix.Domain.Entities;

namespace Netflix.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Lista> Listas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración para el Email único
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Configuración para la tabla Lista (Muchos a Muchos lógica)
        modelBuilder.Entity<Lista>()
            .HasOne(l => l.Profile)
            .WithMany(p => p.MyList)
            .HasForeignKey(l => l.ProfileId);
    }
}