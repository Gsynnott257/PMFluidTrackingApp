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

public partial class GreaseViewModel : ObservableObject
{
    [ObservableProperty]
    private string _mcnumber;
    [ObservableProperty]
    private string _grease;
    [ObservableProperty]
    private string _greaseselection;
    [ObservableProperty]
    private string _greaseadded;
    [ObservableProperty]
    private bool _expandmode = false;

    readonly ISearchCoolantRepository searchCoolantService = new ISearchCoolantService();

    public GreaseViewModel(ISearchCoolantRepository searchCoolantService)
    {
        this.searchCoolantService = searchCoolantService;
    }

    [RelayCommand]
    public async Task SearchAsync()
    {
        Coolant coolant = await searchCoolantService.GetCoolant(Mcnumber);

        if (coolant != null)
        {
            if (coolant.Grease == null)
            {
                await Shell.Current.DisplayAlert("Error", "Machine Number scanned does not have any grease options. Please scan a different machine.", "Ok");
                return;
            }
            Mcnumber = coolant.MC_Num;
            Grease = coolant.Grease;
            Expandmode = true;
        }
    }

    [RelayCommand]
    public async void SubmitAsync()
    {
        try
        {
            float i = 0;
            if (Mcnumber.Length != 7)
            {
                await Shell.Current.DisplayAlert("Error", "Machine number is incorrect length. Machine number must be 6 digits.", "Ok");
                return;
            }
            if (Greaseselection == null)
            {
                await Shell.Current.DisplayAlert("Error", "Please select the grease option that was measured.", "Ok");
                return;
            }
            if (Greaseadded == null)
            {
                await Shell.Current.DisplayAlert("Error", "Please enter if grease was added or not.", "Ok");
                return;
            }

            var MachineCheck = await searchCoolantService.GetCoolant(Mcnumber);
            if (MachineCheck == null)
            {
                await Shell.Current.DisplayAlert("Error", "Machine not found in the database", "Ok");
                return;
            }

            GreaseMeasurement greaseMeasurement = new GreaseMeasurement
            {
                MC_Num = Mcnumber,
                Grease_Type = Grease,
                Grease_Selection = Greaseselection,
                Grease_Added = Greaseadded,
                User_Name = App.user.Name,
            };
            await searchCoolantService.SubmitGreaseData(greaseMeasurement);
            await Shell.Current.DisplayAlert("Submitted", "Grease Measurement Submitted", "Ok");
            Mcnumber = null;
            Grease = null;
            Greaseselection = null;
            Greaseadded = null;
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
        Grease = null;
        Greaseselection = null;
        Greaseadded = null;
        Expandmode = false;
    }
}
