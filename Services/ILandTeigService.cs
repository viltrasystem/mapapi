using ViltrapportenApi.Data.MapModels;
using ViltrapportenApi.Modal;

namespace ViltrapportenApi.Services
{
    public interface ILandTeigService
    {
        LandTeigDTO GetLandTeigDataFromLandTable(LandDrawn land);
        LandTeigDTO GetLandTeigDataFromTeigTable(Teig teig);
    }
}
