using Microsoft.EntityFrameworkCore;
using Plataforma_Aprendizaje.Data;
using Plataforma_Aprendizaje.Models;

namespace Plataforma_Aprendizaje.Views;

[QueryProperty(nameof(CategoriaInicial), "categoria")]
public partial class CursosPage : ContentPage
{
    private string categoriaSeleccionada = "Todos";
    private List<Curso> cursos = new();

public string CategoriaInicial { get; set; } = "Todos";

    public CursosPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (CategoriaInicial == "Tecnología" ||
            CategoriaInicial == "Diseño" ||
            CategoriaInicial == "Todos")
        {
            categoriaSeleccionada = CategoriaInicial;
        }
        else
        {
            categoriaSeleccionada = "Todos";
        }

        BuscadorCursos.Text = string.Empty;

        await CargarCursosAsync();
    }

    private async Task CargarCursosAsync()
    {
        try
        {
            await DatabaseInitializer.InicializarAsync();

            using var db = new AppDbContext();

            // Consulta LINQ: obtiene los cursos y sus categorías.
            cursos = await db.Cursos
                .Include(c => c.Categoria)
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .ToListAsync();

            ActualizarTarjetas();
            FiltrarCursos();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Error al cargar los cursos: {ex}");

            await DisplayAlert(
                "Error",
                "No fue posible cargar los cursos de la base de datos.",
                "Aceptar");
        }
    }

    private void ActualizarTarjetas()
    {
        foreach (var curso in cursos)
        {
            switch (curso.Id)
            {
                case 1:
                    NombreSoftwareLabel.Text = curso.Nombre;
                    DescripcionSoftwareLabel.Text = curso.Descripcion;
                    break;

                case 2:
                    NombreRedesLabel.Text = curso.Nombre;
                    DescripcionRedesLabel.Text = curso.Descripcion;
                    break;

                case 3:
                    NombreBasesDatosLabel.Text = curso.Nombre;
                    DescripcionBasesDatosLabel.Text = curso.Descripcion;
                    break;

                case 4:
                    NombreDisenoLabel.Text = curso.Nombre;
                    DescripcionDisenoLabel.Text = curso.Descripcion;
                    break;
            }
        }
    }

    private void OnBuscadorCursosTextChanged(
        object sender,
        TextChangedEventArgs e)
    {
        FiltrarCursos();
    }

    private void OnCategoriaClicked(
        object sender,
        EventArgs e)
    {
        if (sender is Button boton &&
            boton.CommandParameter is string categoria)
        {
            categoriaSeleccionada = categoria;
            FiltrarCursos();
        }
    }

    private void FiltrarCursos()
    {
        string texto = BuscadorCursos.Text?.Trim() ?? string.Empty;

        var cursosFiltrados = cursos
            .Where(curso =>
                categoriaSeleccionada == "Todos" ||
                string.Equals(
                    curso.Categoria?.Nombre,
                    categoriaSeleccionada,
                    StringComparison.OrdinalIgnoreCase))
            .Where(curso =>
                string.IsNullOrWhiteSpace(texto) ||
                curso.Nombre.Contains(
                    texto,
                    StringComparison.OrdinalIgnoreCase) ||
                curso.Descripcion.Contains(
                    texto,
                    StringComparison.OrdinalIgnoreCase) ||
                (curso.Categoria?.Nombre.Contains(
                    texto,
                    StringComparison.OrdinalIgnoreCase) ?? false))
            .Select(curso => curso.Id)
            .ToHashSet();

        TarjetaSoftware.IsVisible = cursosFiltrados.Contains(1);
        TarjetaRedes.IsVisible = cursosFiltrados.Contains(2);
        TarjetaBasesDatos.IsVisible = cursosFiltrados.Contains(3);
        TarjetaDiseno.IsVisible = cursosFiltrados.Contains(4);

        bool hayResultados =
            TarjetaSoftware.IsVisible ||
            TarjetaRedes.IsVisible ||
            TarjetaBasesDatos.IsVisible ||
            TarjetaDiseno.IsVisible;

        EstadoSinResultados.IsVisible = !hayResultados;
    }

    private async void OnVerCursoClicked(
        object sender,
        EventArgs e)
    {
        if (sender is Button boton &&
            boton.CommandParameter is not null)
        {
            string cursoId = boton.CommandParameter.ToString()!;

            await Shell.Current.GoToAsync(
                $"DetalleCursoPage?cursoId={cursoId}");
        }
    }

}
