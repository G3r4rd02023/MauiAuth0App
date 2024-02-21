using MauiAuth0App.Auth0;
using MauiAuth0App.Pages;
using MauiAuth0App.Services;

namespace MauiAuth0App
{
    public partial class MainPage : ContentPage
    {

        private readonly Auth0Client auth0Client;
        private readonly IServicioRoles _servicioRoles;
        private readonly IServicioInstituto _servicioInstituto;
        private readonly IServicioTipoPersona _servicioTipoPersona;
        private readonly IServicioUsuario _servicioUsuario;


        public MainPage(Auth0Client client, IServicioRoles servicioRoles, IServicioInstituto servicioInstituto,
            IServicioTipoPersona servicioTipoPersona, IServicioUsuario servicioUsuario)
        {
            InitializeComponent();
            auth0Client = client;
            _servicioRoles = servicioRoles;
            _servicioInstituto = servicioInstituto;
            _servicioTipoPersona = servicioTipoPersona;
            _servicioUsuario = servicioUsuario;
        }


        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var loginResult = await auth0Client.LoginAsync();

            if (!loginResult.IsError)
            {

                UsernameLbl.Text = loginResult.User.Identity!.Name;
                var usuario = UsernameLbl.Text;                
                UserPictureImg.Source = loginResult.User
                .Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

                bool isFirstLogin = await _servicioUsuario.ValidarPrimerLogin(usuario!);

                if (isFirstLogin)
                {
                    await Navigation.PushAsync(new RegistroPage(_servicioRoles, _servicioInstituto, _servicioTipoPersona,usuario!));
                    LoginView.IsVisible = false;
                    HomeView.IsVisible = true;
                }
                else
                {
                    LoginView.IsVisible = false;
                    HomeView.IsVisible = true;
                }
                int idUsuario = await _servicioRoles.ObtenerIdUsuario(usuario!);
                ServicioBitacora.AgregarRegistro(idUsuario, 1, "Inicio Sesión", "Sistema");
            }
            else
            {
                await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
            }
        }
    }

}
