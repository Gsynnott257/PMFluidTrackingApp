using PMFluidTrackingApp.ViewModels;

namespace PMFluidTrackingApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}