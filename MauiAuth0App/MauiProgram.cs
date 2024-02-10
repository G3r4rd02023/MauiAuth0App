using MauiAuth0App.Auth0;
using Microsoft.Extensions.Logging;

namespace MauiAuth0App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton(new Auth0Client(new()
            {
                Domain = "dev-i7b5b6jf78s0bn4z.us.auth0.com",
                ClientId = "wIZzNvIhzmcVWAlsmEjDAsCN9E5yYrxy",
                Scope = "openid profile",
                RedirectUri = "myapp://callback"
            }));

            return builder.Build();
        }
    }
}
