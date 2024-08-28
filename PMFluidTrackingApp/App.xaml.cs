using PMFluidTrackingApp.Models;

namespace PMFluidTrackingApp
{
    public partial class App : Application
    {
        public static User user;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
