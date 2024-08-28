using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PMFluidTrackingApp.Views;

namespace PMFluidTrackingApp.ViewModels;

//Need to Build the view page and viewModel in the 
public partial class HomeViewModel : ObservableObject//Needed Observable Object on the end of this line
{
    [RelayCommand] //Tells the program to create a command
    public async void GoToCoolantPage() //Command will be the name of the method + "Command" ex = GoToCoolantPageCommand
    {
        await Shell.Current.GoToAsync("///" + nameof(CoolantPage));
    }
    [RelayCommand]
    public async void GoToOilPage()
    {
        await Shell.Current.GoToAsync("///" + nameof(OilPage));
    }
    [RelayCommand]
    public async void GoToAboutPage()
    {
        await Shell.Current.GoToAsync("///" + nameof(AboutPage));
    }
    [RelayCommand]
    public async void GoToGreasePage()
    {
        await Shell.Current.GoToAsync("///" + nameof(GreasePage));
    }
    [RelayCommand]
    public async void GoToChillerPage()
    {
        await Shell.Current.GoToAsync("///" + nameof(ChillerPage));
    }
    [RelayCommand]
    public async void GoToDistilledWaterPage()
    {
        await Shell.Current.GoToAsync("///" + nameof(DistilledWaterPage));
    }
    [RelayCommand]
    public async void Logout()
    {
        Preferences.Remove(nameof(App.user));
        await Shell.Current.GoToAsync("///" + nameof(LoginPage));
    }
}
