using Microsoft.EntityFrameworkCore;
using Plataforma_Aprendizaje.Models;

namespace Plataforma_Aprendizaje.Data;

public static class DatabaseInitializer
{
    public static async Task InicializarAsync()
    {
        using var db = new AppDbContext();

    // Crear la base de datos y sus tablas si no existen.
    await db.Database.EnsureCreatedAsync();

        // Evitar duplicar los datos iniciales.
        if (await db.Categorias.AnyAsync())
            return;

        var tecnologia = new Categoria
        {
            Nombre = "Tecnología",
            Descripcion = "Cursos de programación, redes y bases de datos."
        };

        var diseno = new Categoria
        {
            Nombre = "Diseño",
            Descripcion = "Cursos de diseño de interfaces y experiencia de usuario."
        };

        db.Categorias.AddRange(tecnologia, diseno);

        db.Cursos.AddRange(
            new Curso
            {
                Nombre = "Desarrollo de Software",
                Descripcion = "Fundamentos de programación y desarrollo de aplicaciones.",
                Precio = 1200,
                Categoria = tecnologia
            },
            new Curso
            {
                Nombre = "Redes de Computadores",
                Descripcion = "Fundamentos de redes, protocolos y configuración.",
                Precio = 900,
                Categoria = tecnologia
            },
            new Curso
            {
                Nombre = "Bases de Datos",
                Descripcion = "Diseño, consultas SQL y administración de bases de datos.",
                Precio = 1000,
                Categoria = tecnologia
            },
            new Curso
            {
                Nombre = "Diseño de Interfaces",
                Descripcion = "Creación de interfaces modernas, intuitivas y accesibles.",
                Precio = 850,
                Categoria = diseno
            }
        );

        await db.SaveChangesAsync();
        int totalCursos = await db.Cursos.CountAsync();
        int totalCategorias = await db.Categorias.CountAsync();

        System.Diagnostics.Debug.WriteLine(
            $"SQLite verificado: {totalCursos} cursos y {totalCategorias} categorías.");
    }

}
