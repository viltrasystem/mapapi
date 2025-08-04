using ViltrapportenApi.Data.CustomModels;
using ViltrapportenApi.Data.SystemModels;

namespace ViltrapportenApi.Services
{
    public interface IDataService
    {
        Task<ViltraUser> GetDataServiceAsync(int userId);
        Task<IList<Municipality>> GetMunicipalityAsync();
    }
}
