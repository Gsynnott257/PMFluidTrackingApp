using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PMFluidTrackingApp.Services;
using PMFluidTrackingApp.ViewModels;
using PMFluidTrackingApp.Views;

namespace PMFluidTrackingApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<AboutPage>();
            builder.Services.AddSingleton<ContactPage>();
            builder.Services.AddSingleton<OilPage>();
            builder.Services.AddSingleton<CoolantPage>();
            builder.Services.AddSingleton<GreasePage>();
            builder.Services.AddSingleton<ChillerPage>();
            builder.Services.AddSingleton<DistilledWaterPage>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<AboutViewModel>();
            builder.Services.AddSingleton<CoolantViewModel>();
            builder.Services.AddSingleton<OilViewModel>();
            builder.Services.AddSingleton<GreaseViewModel>();
            builder.Services.AddSingleton<ChillerViewModel>();
            builder.Services.AddSingleton<DistilledWaterViewModel>();
            builder.Services.AddTransient<ILoginRepository, LoginService>();
            builder.Services.AddTransient<ISearchCoolantRepository, ISearchCoolantService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
