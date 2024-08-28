using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using PMFluidTrackingApp.Models;
using PMFluidTrackingApp.Services;
using PMFluidTrackingApp.Views;

namespace PMFluidTrackingApp.ViewModels;
public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string _email;
    [ObservableProperty]
    private string _password;

    readonly ILoginRepository loginService = new LoginService();

    [RelayCommand]
    public async void Signin()
    {
        if(Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
                {
                    User user = await loginService.Login(Email, Password);
                    
                    if (user != null)
                    {
                        if (Preferences.ContainsKey(nameof(App.user)))
                        {
                            Preferences.Remove(nameof(App.user));
                        }
                        string userDetails = JsonConvert.SerializeObject(user);
                        Preferences.Set(nameof(App.user), userDetails);
                        App.user = user;
                        Email = null;
                        Password = null;
                        await Shell.Current.GoToAsync(nameof(HomePage));
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Email/Password incorrect", "Ok");
                        return;
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "All Fields Required", "Ok");
                    return;
                }
            }
            catch (HttpRequestException httpEx)
            {
                await Shell.Current.DisplayAlert("Connection Error", "Unable to reach the server. Please check your internet connection and try again.", "Ok");
            }
            catch (Exception ex)
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
    [RelayCommand]
    public async void GoToRegisterPage()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}
