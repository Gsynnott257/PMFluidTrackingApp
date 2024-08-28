using PMFluidTrackingApp.ViewModels;

namespace PMFluidTrackingApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}