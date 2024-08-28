using PMFluidTrackingApp.Views;

namespace PMFluidTrackingApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(ContactPage), typeof(ContactPage));
            Routing.RegisterRoute(nameof(CoolantPage), typeof(CoolantPage));
            Routing.RegisterRoute(nameof(OilPage), typeof(OilPage));
            Routing.RegisterRoute(nameof(GreasePage), typeof(GreasePage));
            Routing.RegisterRoute(nameof(ChillerPage), typeof(ChillerPage));
            Routing.RegisterRoute(nameof(DistilledWaterPage), typeof(DistilledWaterPage));
        }
    }
}
