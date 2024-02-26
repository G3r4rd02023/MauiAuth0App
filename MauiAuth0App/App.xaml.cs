using MauiAuth0App.Auth0;
using MauiAuth0App.Pages;

namespace MauiAuth0App
{
    public partial class App : Application
    {
        private readonly Auth0Client auth0Client;
        public App(Auth0Client client)
        {
            InitializeComponent();
            auth0Client = client;
            MainPage = new LoginPage(auth0Client);
        }
    }
}
