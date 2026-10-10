namespace Plataforma_Aprendizaje.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    // Botón para explorar el catálogo completo
    private async void OnCursosClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Principal/CursosPage");
    }

    // Abrir el detalle del curso de software
    private async void OnVerSoftwareClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("DetalleCursoPage?cursoId=1");
    }

    // Abrir el detalle del curso de diseño
    private async void OnVerDisenoClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("DetalleCursoPage?cursoId=4");
    }

    // Buscar cursos desde Inicio
    private async void OnBuscarCursosClicked(object sender, EventArgs e)
    {
        string busqueda = BuscadorCursos.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(busqueda))
        {
            await DisplayAlert(
                "Buscar cursos",
                "Escribe el nombre del curso que deseas encontrar.",
                "Aceptar");

            return;
        }

        await DisplayAlert(
            "Buscar cursos",
            $"Buscaste: {busqueda}\nSe abrirá el catálogo de cursos.",
            "Aceptar");

        await Shell.Current.GoToAsync("//Principal/CursosPage");
    }

    // Abrir el catálogo filtrado por Tecnología
    private async void OnTecnologiaClicked(
        object sender,
        TappedEventArgs e)
    {
        string categoria = Uri.EscapeDataString("Tecnología");

        await Shell.Current.GoToAsync(
            $"//Principal/CursosPage?categoria={categoria}");
    }

    // Abrir el catálogo filtrado por Diseño
    private async void OnDisenoClicked(
        object sender,
        TappedEventArgs e)
    {
        string categoria = Uri.EscapeDataString("Diseño");

        await Shell.Current.GoToAsync(
            $"//Principal/CursosPage?categoria={categoria}");
    }
}
