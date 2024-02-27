using IdentityModel.OidcClient;

namespace MauiAuth0App.Auth0
{
    public class Auth0Client
    {
        private readonly OidcClient oidcClient;

        public Auth0Client(Auth0ClientOptions options)
        {
            oidcClient = new OidcClient(new OidcClientOptions
            {
                Authority = $"https://{options.Domain}",
                ClientId = options.ClientId,
                Scope = options.Scope,
                RedirectUri = options.RedirectUri,
                Browser = options.Browser
            });
        }

        public IdentityModel.OidcClient.Browser.IBrowser Browser
        {
            get
            {
                return oidcClient.Options.Browser;
            }
            set
            {
                oidcClient.Options.Browser = value;
            }
        }

        public async Task<LoginResult> LoginAsync()
        {
            return await oidcClient.LoginAsync();
        }

        public async Task<LogoutResult>LogoutAsync()
        {
            return await oidcClient.LogoutAsync();
        }
    }
}
