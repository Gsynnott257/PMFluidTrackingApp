using PMFluidTrackingApp.ViewModels;

namespace PMFluidTrackingApp.Views;

public partial class GreasePage : ContentPage
{
	public GreasePage(GreaseViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}