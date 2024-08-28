using PMFluidTrackingApp.ViewModels;
namespace PMFluidTrackingApp.Views;

public partial class OilPage : ContentPage
{
	public OilPage(OilViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}