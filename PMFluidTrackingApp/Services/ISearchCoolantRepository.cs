using PMFluidTrackingApp.Data.Models;
using PMFluidTrackingApp.Models;

namespace PMFluidTrackingApp.Services;

public interface ISearchCoolantRepository
{
    Task<Coolant> GetCoolant(string MCNum);
    //Task<Oil> GetOil(string MCNum);
    Task<CoolantMeasurement> SubmitCoolantData(CoolantMeasurement coolantMeasurement);
    Task<OilMeasurement> SubmitOilData(OilMeasurement oilMeasurement);
    Task<ChillerMeasurement> SubmitChillerData(ChillerMeasurement chillerMeasurement);
    Task<GreaseMeasurement> SubmitGreaseData(GreaseMeasurement greaseMeasurement);
    Task<DistilledWaterMeasurement> SubmitDistilledWaterData(DistilledWaterMeasurement distilledWaterMeasurement);
}
