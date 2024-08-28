using Microsoft.Maui.ApplicationModel.Communication;
using PMFluidTrackingApp.Data.Models;
using PMFluidTrackingApp.Models;
using System.Net.Http.Json;

namespace PMFluidTrackingApp.Services;

public class ISearchCoolantService : ISearchCoolantRepository
{
    public async Task<Coolant> GetCoolant(string MCNum)
    {
        try
        {
            var client = new HttpClient();
            string url = "http://10.170.50.109:5223/api/coolant/search/" + MCNum;
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                Coolant coolant = await response.Content.ReadFromJsonAsync<Coolant>();
                return await Task.FromResult(coolant);
            }
            return null;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            return null;
        }
    }

    public async Task<CoolantMeasurement> SubmitCoolantData(CoolantMeasurement coolantMeasurement)
    {
        try
        {
            var client = new HttpClient();
            string url = "http://10.170.50.109:5223/api/coolant/" + coolantMeasurement.MC_Num + "/" + coolantMeasurement.Fluid_Name + "/" + coolantMeasurement.Conc_Min
                + "/" + coolantMeasurement.Conc_Max + "/" + coolantMeasurement.Coolant_Recorded + "/" + coolantMeasurement.Action_Taken + "/" + coolantMeasurement.User_Name + "/" + coolantMeasurement.Remeasured_Value;
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, coolantMeasurement);
            response.EnsureSuccessStatusCode();
            return await Task.FromResult(coolantMeasurement);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            return null;
        }
    }

    public async Task<OilMeasurement> SubmitOilData(OilMeasurement oilMeasurement)
    {
        try
        {
            var client = new HttpClient();
            string url = "http://10.170.50.109:5223/api/oilmeasurements/" + oilMeasurement.MC_Num + "/" + oilMeasurement.Oil_Type + "/" + oilMeasurement.Oil_Selection + "/" + 
                                                                oilMeasurement.Oil_Added + "/" + oilMeasurement.New_Oil_Level + "/" + oilMeasurement.User_Name;
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, oilMeasurement);
            response.EnsureSuccessStatusCode();
            return await Task.FromResult(oilMeasurement);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            return null;
        }
    }

    public async Task<ChillerMeasurement> SubmitChillerData(ChillerMeasurement chillerMeasurement)
    {
        try
        {
            var client = new HttpClient();
            string url = "http://10.170.50.109:5223/api/chillermeasurements/" + chillerMeasurement.MC_Num + "/" + chillerMeasurement.Chiller_Type + "/" + chillerMeasurement.Chiller_Selection
                + "/" + chillerMeasurement.Chiller_Added + "/" + chillerMeasurement.User_Name;
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, chillerMeasurement);
            response.EnsureSuccessStatusCode();
            return await Task.FromResult(chillerMeasurement);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            return null;
        }
    }

    public async Task<GreaseMeasurement> SubmitGreaseData(GreaseMeasurement greaseMeasurement)
    {
        try
        {
            var client = new HttpClient();
            string url = "http://10.170.50.109:5223/api/greasemeasurements/" + greaseMeasurement.MC_Num + "/" + greaseMeasurement.Grease_Type + "/" + greaseMeasurement.Grease_Selection
                + "/" + greaseMeasurement.Grease_Added + "/" + greaseMeasurement.User_Name;
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, greaseMeasurement);
            response.EnsureSuccessStatusCode();
            return await Task.FromResult(greaseMeasurement);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            return null;
        }
    }

    public async Task<DistilledWaterMeasurement> SubmitDistilledWaterData(DistilledWaterMeasurement distilledWaterMeasurement)
    {
        try
        {
            var client = new HttpClient();
            string url = "http://10.170.50.109:5223/api/distilledwatermeasurements/" + distilledWaterMeasurement.MC_Num + "/" + distilledWaterMeasurement.Distilled_Water_Selection
                + "/" + distilledWaterMeasurement.Distilled_Water_Added + "/" + distilledWaterMeasurement.User_Name;
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, distilledWaterMeasurement);
            response.EnsureSuccessStatusCode();
            return await Task.FromResult(distilledWaterMeasurement);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            return null;
        }
    }
}
