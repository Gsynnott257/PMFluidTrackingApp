//using Newtonsoft.Json;
using PMFluidTrackingApp.Models;
using System.Diagnostics;
using System.Net.Http.Json;

namespace PMFluidTrackingApp.Services;

public class LoginService : ILoginRepository
{
    public async Task<User> Login(string email, string password)
    {
        try
        {
            var client = new HttpClient();
            //await Shell.Current.DisplayAlert("Error", "Here", "Ok");
            string url = "http://10.170.50.109:5223/api/users/login/" + email + "/" + password;
            //await Shell.Current.DisplayAlert("Error", "Here2", "Ok");
            client.BaseAddress = new Uri(url);
            //await Shell.Current.DisplayAlert("Error", $"Request URL: {client.BaseAddress}", "Ok");
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            //await Shell.Current.DisplayAlert("Error", JsonConvert.SerializeObject(response), "Ok");
            if (response.IsSuccessStatusCode)
            {
                User user = await response.Content.ReadFromJsonAsync<User>();
                return await Task.FromResult(user);
            }
            else
            {
                //await Shell.Current.DisplayAlert("Error", JsonConvert.SerializeObject(response) + " Here", "Ok");
                await Shell.Current.DisplayAlert("Error", $"HTTP Error: {response.StatusCode}, {response.ReasonPhrase}", "Ok");
                //Debug.WriteLine($"HTTP Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
            return null;
        }
        catch (HttpRequestException httpEx)
        {
            Debug.WriteLine($"Request Exception: {httpEx.Message}");
            Debug.WriteLine($"Request Exception (InnerException): {httpEx.InnerException?.Message}");
            await Shell.Current.DisplayAlert("Error", "Unable to reach the server. Please check your internet connection and try again.", "Ok");
            return null;
        }
        catch (TaskCanceledException taskEx)
        {
            Debug.WriteLine($"Request Timed Out: {taskEx.Message}");
            await Shell.Current.DisplayAlert("Error", "The request timed out. Please try again.", "Ok");
            return null;
        }
        catch (Exception ex)
        {
            //await Shell.Current.DisplayAlert("Error", "Here", "Ok");
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            return null;
        }
    }

    public async Task<User> Register(User user)
    {
        try
        {
            var client = new HttpClient();
            string url = "http://10.170.50.109:5223/api/users/Register/" + user.Name + "/" + user.Email + "/" + user.Password;
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, user);
            response.EnsureSuccessStatusCode();
            return await Task.FromResult(user);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            return null;
        }
    }
}
