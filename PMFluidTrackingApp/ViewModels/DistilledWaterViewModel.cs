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

public partial class DistilledWaterViewModel : ObservableObject
{
    [ObservableProperty]
    private string _mcnumber;
    [ObservableProperty]
    private string _distilledwaterselection;
    [ObservableProperty]
    private string _distilledwateradded;
    [ObservableProperty]
    private bool _expandmode = false;

    readonly ISearchCoolantRepository searchCoolantService = new ISearchCoolantService();

    public DistilledWaterViewModel(ISearchCoolantRepository searchCoolantService)
    {
        this.searchCoolantService = searchCoolantService;
    }

    [RelayCommand]
    public async Task SearchAsync()
    {
        Coolant coolant = await searchCoolantService.GetCoolant(Mcnumber);

        if (coolant != null)
        {
            if (coolant.Distilled_Water == null)
            {
                await Shell.Current.DisplayAlert("Error", "Machine Number scanned does not have any distilled water options. Please scan a different machine.", "Ok");
                return;
            }
            Mcnumber = coolant.MC_Num;
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
            if (Distilledwaterselection == null)
            {
                await Shell.Current.DisplayAlert("Error", "Please select the Chiller level that was completed.", "Ok");
                return;
            }
            if (Distilledwateradded == null)
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

            DistilledWaterMeasurement distilledWaterMeasurement = new DistilledWaterMeasurement
            {
                MC_Num = Mcnumber,
                Distilled_Water_Selection = Distilledwaterselection,
                Distilled_Water_Added = Distilledwateradded,
                User_Name = App.user.Name,
            };
            await searchCoolantService.SubmitDistilledWaterData(distilledWaterMeasurement);
            await Shell.Current.DisplayAlert("Submitted", "Distilled Water Measurement Submitted", "Ok");
            Mcnumber = null;
            Distilledwaterselection = null;
            Distilledwateradded = null;
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
        Distilledwaterselection = null;
        Distilledwateradded = null;
        Expandmode = false;
    }
}
