using MauiAuth0App.Auth0;
using MauiAuth0App.Pages;
using Microsoft.Maui.Controls.PlatformConfiguration;

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
                Application.Current.MainPage = new MainPage(auth0Client);
                Shell.SetNavBarIsVisible(this, true);
            }
        }
    }
}
