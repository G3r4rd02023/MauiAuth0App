using MauiAuth0App.Auth0;
using MauiAuth0App.Services;

namespace MauiAuth0App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var options = new Auth0ClientOptions
            {
                Domain = "dev-i7b5b6jf78s0bn4z.us.auth0.com",
                ClientId = "wIZzNvIhzmcVWAlsmEjDAsCN9E5yYrxy",
                // Otros parámetros según sea necesario
            };

            var auth0Client = new Auth0Client(options);

            IServicioRoles servicioRoles = new ServicioRoles();            
            IServicioInstituto servicioInstituto = new ServicioInstituto();
            IServicioTipoPersona servicioTipoPersona = new ServicioTipoPersona();

            MainPage mainPage = new MainPage(auth0Client, servicioRoles,servicioInstituto,servicioTipoPersona);


            MainPage = new NavigationPage(mainPage);
        }
    }
}
