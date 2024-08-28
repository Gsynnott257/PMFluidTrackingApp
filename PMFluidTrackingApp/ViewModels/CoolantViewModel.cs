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

public partial class CoolantViewModel : ObservableObject
{
    [ObservableProperty]
    private string _mcnumber;
    [ObservableProperty]
    private string _fluidname;
    [ObservableProperty]
    private string _minconc;
    [ObservableProperty]
    private string _maxconc;
    [ObservableProperty]
    private string _measuredValue;
    [ObservableProperty]
    private string _tpmSelection;
    [ObservableProperty]
    private string _remeasuredValue;
    [ObservableProperty]
    private bool _expandmode = false;

    public ObservableCollection<string> TpmOptions { get; set; } = new ObservableCollection<string>();

    readonly ISearchCoolantRepository searchCoolantService = new ISearchCoolantService();

    public CoolantViewModel(ISearchCoolantRepository searchCoolantService)
    {
        this.searchCoolantService = searchCoolantService;
    }

    [RelayCommand]
    public async Task SearchAsync()
    {
        Coolant coolant = await searchCoolantService.GetCoolant(Mcnumber);

        if (coolant != null)
        {
            if (coolant.TPM1 == null)
            {
                await Shell.Current.DisplayAlert("Error", "Machine Number scanned does not have any coolant options. Please scan a different machine.", "Ok");
                return;
            }
            Mcnumber = coolant.MC_Num;
            Fluidname = coolant.Fluid_Name;

            float.TryParse(coolant.Conc_Min, out float minconcValue);
            minconcValue = minconcValue * 100;
            string MinPercent = minconcValue.ToString("F2") + "%";
            Minconc = MinPercent;

            float.TryParse(coolant.Conc_Max, out float maxconcValue);
            maxconcValue = maxconcValue * 100;
            string MaxPercent = maxconcValue.ToString("F2") + "%";
            Maxconc = MaxPercent;

            //Minconc = coolant.Conc_Min;
            //Maxconc = coolant.Conc_Max;
            TpmOptions.Clear();
            TpmOptions.Add(coolant.TPM1);
            if (coolant.TPM2 != null)
            {
                TpmOptions.Add(coolant.TPM2);
            }
            if (coolant.TPM3 != null)
            {
                TpmOptions.Add(coolant.TPM3);
            }
            if (coolant.TPM4 != null)
            {
                TpmOptions.Add(coolant.TPM4);
            }
            if (coolant.TPM5 != null)
            {
                TpmOptions.Add(coolant.TPM5);
            }
            if (coolant.TPM6 != null)
            {
                TpmOptions.Add(coolant.TPM6);
            }
            TpmSelection = null;
            if (coolant.TPM2 == null && coolant.TPM3 == null && coolant.TPM4 == null && coolant.TPM5 == null && coolant.TPM6 == null)
            {
                TpmSelection = coolant.TPM1;
            }
            Expandmode = true;
            MeasuredValue = null;
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
            if (!float.TryParse(MeasuredValue, out i))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter a number", "Ok");
                return;
            }
            //if (float.Parse(MeasuredValue) < float.Parse(Minconc.Replace("%", "").Trim()) && TpmSelection == null)
            //{
            //    await Shell.Current.DisplayAlert("Error", "Measured value out of spec. Please select TPM Option.", "Ok");
            //    return;
            //}
            //if (float.Parse(MeasuredValue) > float.Parse(Maxconc.Replace("%", "").Trim()) && TpmSelection == null)
            //{
            //    await Shell.Current.DisplayAlert("Error", "Measured value out of spec. Please select TPM Option.", "Ok");
            //    return;
            //}
            if (float.Parse(MeasuredValue) < float.Parse(Minconc.Replace("%", "").Trim()) || float.Parse(MeasuredValue) > float.Parse(Maxconc.Replace("%", "").Trim()))
            {
                if (TpmSelection == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Please select the TPM Option that was completed.", "Ok");
                    return;
                }
                if (RemeasuredValue == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Please enter the remeasured value after the TPM was completed.", "Ok");
                    return;
                }
            }

            var MachineCheck = await searchCoolantService.GetCoolant(Mcnumber);
            if (MachineCheck == null)
            {
                await Shell.Current.DisplayAlert("Error", "Machine not found in the database", "Ok");
                return;
            }

            if (TpmSelection == null)
            {
                TpmSelection = "Not Applicable";
            }
            if (RemeasuredValue == null)
            {
                RemeasuredValue = "Not Applicable";
            }
            CoolantMeasurement coolantMeasurement = new CoolantMeasurement
            {
                MC_Num = Mcnumber,
                Fluid_Name = Fluidname,
                Conc_Min = Minconc,
                Conc_Max = Maxconc,
                Coolant_Recorded = MeasuredValue,
                Action_Taken = TpmSelection,
                User_Name = App.user.Name,
                Remeasured_Value = RemeasuredValue,
            };
            await searchCoolantService.SubmitCoolantData(coolantMeasurement);
            await Shell.Current.DisplayAlert("Submitted", "Coolant Measurement Submitted", "Ok");
            Mcnumber = null;
            Fluidname = null;
            Minconc = null;
            Maxconc = null;
            MeasuredValue = null;
            TpmSelection = null;
            RemeasuredValue = null;
            TpmOptions.Clear();
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
        Fluidname = null;
        Minconc = null;
        Maxconc = null;
        MeasuredValue = null;
        TpmSelection = null;
        RemeasuredValue = null;
        Expandmode = false;
    }
}
