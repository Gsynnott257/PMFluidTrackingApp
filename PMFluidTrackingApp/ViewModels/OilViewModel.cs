using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using PMFluidTrackingApp.Data.Models;
using PMFluidTrackingApp.Models;
using PMFluidTrackingApp.Services;
using PMFluidTrackingApp.Views;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace PMFluidTrackingApp.ViewModels;

public partial class OilViewModel : ObservableObject
{
    [ObservableProperty]
    private string _mcnumber;
    /*[ObservableProperty]
    private string _mnum = Mcnumber;*/
    [ObservableProperty]
    private string _oiltype;
    [ObservableProperty]
    private string _oilselection;
    [ObservableProperty]
    private string _oiladded;
    [ObservableProperty]
    private string _newoillevel;
    [ObservableProperty]
    private string _measuredValue;
    [ObservableProperty]
    private string _tpmSelection;
    [ObservableProperty]
    private bool _expandmode = false;

    public ObservableCollection<string> OilOptions { get; set; } = new ObservableCollection<string>();

    readonly ISearchCoolantRepository searchCoolantService = new ISearchCoolantService();

    public OilViewModel(ISearchCoolantRepository searchCoolantService)
    {
        this.searchCoolantService = searchCoolantService;
    }

    [RelayCommand]
    public async Task SearchAsync()
    {
        Coolant oil = await searchCoolantService.GetCoolant(Mcnumber);

        if (oil != null)
        {
            if (oil.Oil1 == null)
            {
                await Shell.Current.DisplayAlert("Error", "Machine Number scanned does not have any oil options. Please scan a different machine.", "Ok");
                return;
            }
            Mcnumber = oil.MC_Num;
            OilOptions.Clear();
            OilOptions.Add(oil.Oil1);
            if (oil.Oil2 != null)
            {
                OilOptions.Add(oil.Oil2);
            }
            if (oil.Oil3 != null)
            {
                OilOptions.Add(oil.Oil3);
            }
            if (oil.Oil4 != null)
            {
                OilOptions.Add(oil.Oil4);
            }
            if (oil.Oil5 != null)
            {
                OilOptions.Add(oil.Oil5);
            }
            if (oil.Oil6 != null)
            {
                OilOptions.Add(oil.Oil6);
            }
            if (oil.Oil2 == null && oil.Oil3 == null && oil.Oil4 == null && oil.Oil5 == null && oil.Oil6 == null)
            {
                Oiltype = oil.Oil1;
            }
            Expandmode = true;
            MeasuredValue = null;
            //TpmSelection = null;
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
            if (Oiltype == null)
            {
                await Shell.Current.DisplayAlert("Error", "Please select the Oil Type", "Ok");
                return;
            }
            if (Oilselection == null)
            {
                await Shell.Current.DisplayAlert("Error", "Please select an option for Oil Recorded", "Ok");
                return;
            }
            if (!float.TryParse(Oiladded, out i))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter a number into oil added", "Ok");
                return;
            }

            var MachineCheck = await searchCoolantService.GetCoolant(Mcnumber);
            if (MachineCheck == null) 
            {
                await Shell.Current.DisplayAlert("Error", "Machine not found in the database", "Ok");
                return;
            }

            OilMeasurement oilMeasurement = new OilMeasurement
            {
                MC_Num = Mcnumber,
                Oil_Type = Oiltype,
                Oil_Selection = Oilselection,
                Oil_Added = Oiladded,
                New_Oil_Level = Oiladded,
                User_Name = App.user.Name,
            };
            await searchCoolantService.SubmitOilData(oilMeasurement);
            await Shell.Current.DisplayAlert("Submitted", "Oil Measurements Submitted", "Ok");
            Mcnumber = null;
            Oiltype = null;
            Oilselection = null;
            Oiladded = null;
            OilOptions.Clear();
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
        Oiltype = null;
        Oilselection = null;
        Oiladded = null;
        OilOptions.Clear();
        Expandmode = false;
    }
}
