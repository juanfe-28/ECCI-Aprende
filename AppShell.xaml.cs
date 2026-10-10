using Plataforma_Aprendizaje.Views;

namespace Plataforma_Aprendizaje;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(
            "HomePage",
            typeof(HomePage));

        Routing.RegisterRoute(
            "CursosPage",
            typeof(CursosPage));

        Routing.RegisterRoute(
            "DetalleCursoPage",
            typeof(DetalleCursoPage));
    }
}