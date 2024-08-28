using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
//using Newtonsoft.Json;
using PMFluidTrackingApp.Models;
using PMFluidTrackingApp.Services;
using PMFluidTrackingApp.Views;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace PMFluidTrackingApp.ViewModels;

public partial class AboutViewModel : ObservableObject
{

    /*readonly ISearchCoolantRepository searchCoolantService = new ISearchCoolantService();
    public AboutViewModel(ISearchCoolantRepository searchCoolantService)
    {
        this.searchCoolantService = searchCoolantService;
    }*/
    [RelayCommand]
    public async void GoToHomePage()
    {
        await Shell.Current.GoToAsync("///" + nameof(HomePage));
    }
}
