using Plataforma_Aprendizaje.Data;

namespace Plataforma_Aprendizaje
{
    public partial class App : Application
    {
        private static bool baseDatosInicializada;

    public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(
            IActivationState? activationState)
        {
            var ventana = new Window(new AppShell());

            ventana.Created += async (sender, args) =>
            {
                if (baseDatosInicializada)
                    return;

                try
                {
                    await DatabaseInitializer.InicializarAsync();
                    baseDatosInicializada = true;
                    System.Diagnostics.Debug.WriteLine(
                        "Base de datos inicializada correctamente.");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"Error al inicializar la base de datos: {ex}");
                }
            };

            return ventana;
        }
    }

}
