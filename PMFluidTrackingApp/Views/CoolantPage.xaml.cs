using PMFluidTrackingApp.ViewModels;

namespace PMFluidTrackingApp.Views;

public partial class CoolantPage : ContentPage
{
	public CoolantPage(CoolantViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}