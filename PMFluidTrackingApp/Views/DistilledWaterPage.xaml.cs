using PMFluidTrackingApp.ViewModels;

namespace PMFluidTrackingApp.Views;

public partial class DistilledWaterPage : ContentPage
{
	public DistilledWaterPage(DistilledWaterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}