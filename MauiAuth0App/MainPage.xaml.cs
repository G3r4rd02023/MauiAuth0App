using MauiAuth0App.Auth0;
using MauiAuth0App.Pages;
using MauiAuth0App.Services;

namespace MauiAuth0App
{
    public partial class MainPage : ContentPage
    {

        private readonly Auth0Client auth0Client;
        public static string? UsuarioAutenticado { get; set; }


        public MainPage(Auth0Client client)
        {
            InitializeComponent();
            auth0Client = client;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var loginResult = await auth0Client.LoginAsync();


            if (!loginResult.IsError)
            {

                UsernameLbl.Text = loginResult.User.Identity!.Name;
                UsuarioAutenticado = loginResult.User.Identity!.Name;
                var usuario = UsernameLbl.Text;
                UserPictureImg.Source = loginResult.User
                .Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

                ServicioUsuario servicioUsuario = new();

                bool isFirstLogin = await servicioUsuario.ValidarPrimerLogin(usuario!);

                if (isFirstLogin)
                {
                    
                    await Navigation.PushAsync(new RegistroPage(usuario!));
                    LoginView.IsVisible = false;
                    HomeView.IsVisible = true;
                    //App.Current.MainPage = new AppShell();
                }
                else
                {

                    LoginView.IsVisible = false;
                    HomeView.IsVisible = true;
                    //App.Current.MainPage = new AppShell();
                }

                int idUsuario = await ServicioRoles.ObtenerIdUsuario(usuario!);
                ServicioBitacora.AgregarRegistro(idUsuario, 1, "Inicio Sesión", "Sistema");
            }
            else
            {
                await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
            }
        }

    }

}

//Listooooo pruebela en su celllllll   