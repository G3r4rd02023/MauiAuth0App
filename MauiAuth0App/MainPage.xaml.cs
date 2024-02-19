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


        public MainPage(Auth0Client client, IServicioRoles servicioRoles, IServicioInstituto servicioInstituto,
            IServicioTipoPersona servicioTipoPersona)
        {
            InitializeComponent();
            auth0Client = client;
            _servicioRoles = servicioRoles;
            _servicioInstituto = servicioInstituto;
            _servicioTipoPersona = servicioTipoPersona;
        }


        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var loginResult = await auth0Client.LoginAsync();

            if (!loginResult.IsError)
            {
                bool isFirstLogin = true;

                if (isFirstLogin)
                {
                    // Es el primer inicio de sesión, redirige al usuario a la página de registro
                    await Navigation.PushAsync(new RegistroPage(_servicioRoles, _servicioInstituto, _servicioTipoPersona));
                }
                else
                {
                    UsernameLbl.Text = loginResult.User.Identity!.Name;
                    UserPictureImg.Source = loginResult.User
                    .Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

                    LoginView.IsVisible = false;
                    HomeView.IsVisible = true;
                }
            }
            else
            {
                await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
            }
        }
    }

}
