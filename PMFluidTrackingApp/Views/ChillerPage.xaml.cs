using PMFluidTrackingApp.ViewModels;

namespace PMFluidTrackingApp.Views;

public partial class ChillerPage : ContentPage
{
	public ChillerPage(ChillerViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}