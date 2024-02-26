using MauiAuth0App.Auth0;

namespace MauiAuth0App
{
    public partial class App : Application
    {
        private readonly Auth0Client auth0Client;
        public App(Auth0Client client)
        {
            InitializeComponent();
            auth0Client = client;
            MainPage = new AppShell(auth0Client);

            

        }
    }
}
