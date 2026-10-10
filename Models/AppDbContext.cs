using Microsoft.EntityFrameworkCore;
using System.IO;
using Plataforma_Aprendizaje.Models;

namespace Plataforma_Aprendizaje.Data;

public class AppDbContext : DbContext
{
    public DbSet<Curso> Cursos => Set<Curso>();

public DbSet<Categoria> Categorias => Set<Categoria>();

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string rutaBase = FileSystem.AppDataDirectory;
            string rutaBaseDatos = Path.Combine(
                rutaBase,
                "ecci_aprende.db");

            optionsBuilder.UseSqlite(
                $"Data Source={rutaBaseDatos}");
        }
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Categoria>()
            .HasMany(c => c.Cursos)
            .WithOne(c => c.Categoria)
            .HasForeignKey(c => c.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}
