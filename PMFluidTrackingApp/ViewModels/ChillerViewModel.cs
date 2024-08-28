using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using PMFluidTrackingApp.Models;
using PMFluidTrackingApp.Services;
using PMFluidTrackingApp.Views;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace PMFluidTrackingApp.ViewModels;

public partial class ChillerViewModel : ObservableObject
{
    [ObservableProperty]
    private string _mcnumber;
    [ObservableProperty]
    private string _chiller;
    [ObservableProperty]
    private string _chillerselection;
    [ObservableProperty]
    private string _chilleradded;
    [ObservableProperty]
    private bool _expandmode = false;

    readonly ISearchCoolantRepository searchCoolantService = new ISearchCoolantService();

    public ChillerViewModel(ISearchCoolantRepository searchCoolantService)
    {
        this.searchCoolantService = searchCoolantService;
    }

    [RelayCommand]
    public async Task SearchAsync()
    {
        Coolant coolant = await searchCoolantService.GetCoolant(Mcnumber);

        if (coolant != null)
        {
            if (coolant.Chiller == null)
            {
                await Shell.Current.DisplayAlert("Error", "Machine Number scanned does not have any chiller options. Please scan a different machine.", "Ok");
                return;
            }
            Mcnumber = coolant.MC_Num;
            Chiller = coolant.Chiller;
            Expandmode = true;
        }
    }

    [RelayCommand]
    public async void SubmitAsync()
    {
        try
        {
            if (Mcnumber.Length != 7)
            {
                await Shell.Current.DisplayAlert("Error", "Machine number is incorrect length. Machine number must be 6 digits.", "Ok");
                return;
            }
            if (Chillerselection == null)
            {
                await Shell.Current.DisplayAlert("Error", "Please select the Chiller level that was completed.", "Ok");
                return;
            }
            if (Chilleradded == null)
            {
                await Shell.Current.DisplayAlert("Error", "Please select whether Chiller was added.", "Ok");
                return;
            }

            var MachineCheck = await searchCoolantService.GetCoolant(Mcnumber);
            if (MachineCheck == null)
            {
                await Shell.Current.DisplayAlert("Error", "Machine not found in the database", "Ok");
                return;
            }

            ChillerMeasurement chillerMeasurement = new ChillerMeasurement
            {
                MC_Num = Mcnumber,
                Chiller_Type = Chiller,
                Chiller_Selection = Chillerselection,
                Chiller_Added = Chilleradded,
                User_Name = App.user.Name,
            };
            await searchCoolantService.SubmitChillerData(chillerMeasurement);
            await Shell.Current.DisplayAlert("Submitted", "Chiller Measurement Submitted", "Ok");
            Mcnumber = null;
            Chiller = null;
            Chillerselection = null;
            Chilleradded = null;
            Expandmode = false;
        }
        catch (Exception ex) 
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
        }
    }
    [RelayCommand]
    public async void GoToHomePage()
    {
        await Shell.Current.GoToAsync("///" + nameof(HomePage));
        Mcnumber = null;
        Chiller = null;
        Chillerselection = null;
        Chilleradded = null;
        Expandmode = false;
    }
}
