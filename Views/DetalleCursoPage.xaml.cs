using Microsoft.EntityFrameworkCore;
using Plataforma_Aprendizaje.Data;

namespace Plataforma_Aprendizaje.Views;

[QueryProperty(nameof(CursoId), "cursoId")]
public partial class DetalleCursoPage : ContentPage
{
    public string CursoId
    {
        set
        {
            _ = CargarCursoAsync(value);
        }
    }

public DetalleCursoPage()
    {
        InitializeComponent();
    }

    private async Task CargarCursoAsync(string id)
    {
        if (!int.TryParse(Uri.UnescapeDataString(id), out int cursoId))
        {
            await MostrarErrorCursoAsync();
            return;
        }

        try
        {
            using var db = new AppDbContext();

            var curso = await db.Cursos
                .Include(c => c.Categoria)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == cursoId);

            if (curso == null)
            {
                await MostrarErrorCursoAsync();
                return;
            }

            NombreCursoLabel.Text = curso.Nombre;

            DescripcionCortaLabel.Text =
                string.IsNullOrWhiteSpace(curso.Descripcion)
                    ? "Conoce los detalles de este curso."
                    : curso.Descripcion;

            DescripcionLabel.Text = curso.Descripcion;

            string categoria = curso.Categoria?.Nombre ?? "Sin categoría";

            NivelLabel.Text = $"📚 Categoría: {categoria}";

            // Información complementaria temporal.
            // Estos datos aún no forman parte del modelo Curso.
            switch (curso.Id)
            {
                case 1:
                    IconoLabel.Text = "💻";
                    ProfesorLabel.Text = "👨‍🏫 Profesor: Juan Pérez";
                    DuracionLabel.Text = "⏱ Duración: 40 horas";
                    CalificacionLabel.Text = "⭐ Calificación: 4.8 / 5";
                    ContenidoLabel.Text =
                        "✓ Introducción a la programación\n\n" +
                        "✓ Variables y tipos de datos\n\n" +
                        "✓ Condicionales y ciclos\n\n" +
                        "✓ Programación orientada a objetos\n\n" +
                        "✓ Desarrollo de aplicaciones";
                    break;

                case 2:
                    IconoLabel.Text = "🌐";
                    ProfesorLabel.Text = "👨‍🏫 Profesor: Carlos Rodríguez";
                    DuracionLabel.Text = "⏱ Duración: 50 horas";
                    CalificacionLabel.Text = "⭐ Calificación: 4.9 / 5";
                    ContenidoLabel.Text =
                        "✓ Modelo OSI\n\n" +
                        "✓ Direccionamiento IPv4\n\n" +
                        "✓ Switching y VLAN\n\n" +
                        "✓ Routing\n\n" +
                        "✓ Configuración de redes";
                    break;

                case 3:
                    IconoLabel.Text = "🗄️";
                    ProfesorLabel.Text = "👩‍🏫 Profesora: Laura Martínez";
                    DuracionLabel.Text = "⏱ Duración: 35 horas";
                    CalificacionLabel.Text = "⭐ Calificación: 4.7 / 5";
                    ContenidoLabel.Text =
                        "✓ Modelo entidad-relación\n\n" +
                        "✓ Diseño de bases de datos\n\n" +
                        "✓ SQL\n\n" +
                        "✓ Consultas y filtros\n\n" +
                        "✓ Administración de bases de datos";
                    break;

                case 4:
                    IconoLabel.Text = "🎨";
                    ProfesorLabel.Text = "👩‍🏫 Profesora: María Gómez";
                    DuracionLabel.Text = "⏱ Duración: 30 horas";
                    CalificacionLabel.Text = "⭐ Calificación: 4.6 / 5";
                    ContenidoLabel.Text =
                        "✓ Principios de diseño\n\n" +
                        "✓ Experiencia de usuario UX\n\n" +
                        "✓ Diseño visual\n\n" +
                        "✓ Prototipos\n\n" +
                        "✓ Diseño de aplicaciones";
                    break;

                default:
                    IconoLabel.Text = "📘";
                    ProfesorLabel.Text = "👨‍🏫 Profesor: Por definir";
                    DuracionLabel.Text = "⏱ Duración: Por definir";
                    CalificacionLabel.Text = "⭐ Calificación: Sin calificar";
                    ContenidoLabel.Text =
                        "El contenido de este curso está pendiente de publicación.";
                    break;
            }

            Title = curso.Nombre;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Error al consultar el curso: {ex}");

            await DisplayAlert(
                "Error",
                "No fue posible consultar el curso en la base de datos.",
                "Aceptar");
        }
    }

    private async Task MostrarErrorCursoAsync()
    {
        await DisplayAlert(
            "Curso no encontrado",
            "No se encontró el curso solicitado en la base de datos.",
            "Aceptar");

        await Shell.Current.GoToAsync("..");
    }

    private async void OnInscribirmeClicked(
        object sender,
        EventArgs e)
    {
        bool respuesta = await DisplayAlert(
            "Inscripción",
            "¿Deseas inscribirte en este curso?",
            "Sí",
            "No");

        if (respuesta)
        {
            await DisplayAlert(
                "Inscripción exitosa",
                "Te has inscrito correctamente en el curso.",
                "Aceptar");
        }
    }

}
