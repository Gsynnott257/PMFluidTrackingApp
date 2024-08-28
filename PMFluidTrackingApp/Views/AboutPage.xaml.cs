using PMFluidTrackingApp.ViewModels;

namespace PMFluidTrackingApp.Views;

public partial class AboutPage : ContentPage
{
	public AboutPage(AboutViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}