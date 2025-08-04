using GeoJSON.Net;
using ViltrapportenApi.Data.CustomModels;
using ViltrapportenApi.Data.MapModels;
using ViltrapportenApi.Modal;

namespace ViltrapportenApi.Services
{
    public interface IMapDataService
    {
        //Task<List<Hoydekurve>> GetBaseMapServiceAsync();
        Task<List<Eiendomsgrense>> GetMapServiceAsync();
        Task<Result<List<Teig>>> GetSelectedMapServiceAsync(string municipilityNo, int MainNo, int subNo, int? plotNo);
        Task<Result<IList<LandDrawn>>> GetSelectedLandAsync(int landId);
        Task<Result<IList<LandDrawn>>> GetSelectedLandAsync(string municipilityNo, int MainNo, int subNo, int? plotNo);
        Task<List<FeatureDto>> SaveDrawnFeatures(DrawFeatureCollection geoJsonFeatureCollection);
        Task<Result<bool>> SaveModifiedLandFeatures(DrawFeatureCollection geoJsonFeatureCollection);
     //   Task<Result<bool>> SaveLandLayer(int landId, int teigId, int createdBy);
        Task<Result> DeleteLandLayer(DeleteLandLayerReq deleteLandLayerReq);
        Task<List<FeatureDto>> GetDrawnFeatures();
    }
}
