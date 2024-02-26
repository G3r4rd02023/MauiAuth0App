using MauiAuth0App.Auth0;
using MauiAuth0App.Pages;

namespace MauiAuth0App
{
    public partial class AppShell : Shell
    {
        private readonly Auth0Client auth0Client;
        public AppShell(Auth0Client client)
        {
            InitializeComponent();
            auth0Client = client;
        }

        private async void CerrarSesion_Clicked(object sender, EventArgs e)
        {
            bool answer = await Shell.Current.DisplayAlert("Mensaje", "Desea salir?", "Si, continuar", "No, volver");
            if (answer)
            {

                
                Preferences.Remove("accessToken");
                Application.Current.MainPage = new LoginPage(auth0Client);
                //await Navigation.PushAsync(new LoginPage(auth0Client));
            }
        }
    }
}
