using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using PMFluidTrackingApp.Models;
using PMFluidTrackingApp.Services;
using PMFluidTrackingApp.Views;

namespace PMFluidTrackingApp.ViewModels;
public partial class RegisterViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _email;
    [ObservableProperty]
    private string _password;

    readonly ILoginRepository loginService = new LoginService();

    [RelayCommand]
    public async void Register()
    {
        if(Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Name))
                {
                    User user = new User
                    {
                        Email = Email,
                        Password = Password,
                        Name = Name,
                    };
                    await loginService.Register(user);
                    Email = null;
                    Password = null;
                    Name = null;
                    
                    await Shell.Current.GoToAsync("///" + nameof(LoginPage));
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "All Fields Required", "Ok");
                    return;
                }
            }
            catch(Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                return;
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "No Network Access", "Ok");
            return;
        }

    }
}
