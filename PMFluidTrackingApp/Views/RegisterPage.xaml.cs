using PMFluidTrackingApp.ViewModels;

namespace PMFluidTrackingApp.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterViewModel vm)
	{
        InitializeComponent();
		BindingContext = vm;
	}
    protected override void OnAppearing()
    {
        //base.OnAppearing();

        // Focus the EmailEntry when the page appears
        EmailEntry.Focus();
    }
}