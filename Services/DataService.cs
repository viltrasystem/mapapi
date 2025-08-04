using Microsoft.EntityFrameworkCore;
using ViltrapportenApi.Data.CustomModels;
using ViltrapportenApi.Data.SystemModels;

namespace ViltrapportenApi.Services
{
    public class DataService(ViltrapportenSystemContext context) : IDataService
    {
        private readonly ViltrapportenSystemContext _context = context;

        public async Task<ViltraUser> GetDataServiceAsync(int userId)
        {
            return await _context.ViltraUsers.FirstOrDefaultAsync(user => user.DnnUserId == userId);
        }

        public async Task<IList<Municipality>> GetMunicipalityAsync()
        {
            return await _context.Lands.Where(x => x.IsActive).GroupBy(x => new { x.Municipality, x.MunicipalityName })
                    .Select(group => new Municipality
                    {
                        MunicipalityNo = int.Parse(group.Key.Municipality),
                        MunicipalityName = $"{group.Key.MunicipalityName} {group.Key.Municipality}"
                    }).ToListAsync();
        }
    }
}
