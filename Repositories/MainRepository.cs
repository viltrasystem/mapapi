using Microsoft.EntityFrameworkCore;
using ViltrapportenApi.Data.CustomModels;
using ViltrapportenApi.Data.SystemModels;

namespace ViltrapportenApi.Repositories
{
    public class MainRepository
    {
        private ViltrapportenSystemContext _context;
        public MainRepository(ViltrapportenSystemContext context)
        {
            _context = context;
        }


        public async Task<IList<UserUnit>> GetUserUnitsAsync(int dnnUserId, int isAdmin)
        {
            var results = await _context.UserUnits.FromSqlInterpolated($"EXEC GetUserUnitsForStatistics_SP @dnnuserId={dnnUserId}, @isAdmin={isAdmin}").ToListAsync();

            //// 'results' now contains the data returned by the stored procedure
            //foreach (var result in results)
            //{
            //    // Process the data
            //    Console.WriteLine($"{result.Column1} - {result.Column2}");
            //}

            return results;
        }
        
        public async Task<IList<ChildUnitModal>> GetChildNodesAsync(int parentId, bool isUserOnlyOnMunicipality, bool isGuest)
        {
            if (!isUserOnlyOnMunicipality && !isGuest)
            {
                 return  await _context.ChildUnitModals.FromSqlInterpolated($"EXEC GetChildNodes_SP @parentId={parentId}").ToListAsync();
            } else if (isUserOnlyOnMunicipality && isGuest)
            {
                return await _context.ChildUnitModals.FromSqlInterpolated($"EXEC GetChildNodesForMunicipalityUser_SP @parentId={parentId}").ToListAsync();
            }
            else
            {
                return await _context.ChildUnitModals.FromSqlInterpolated($"EXEC GetChildNodesForGuestUser_SP @parentId={parentId}").ToListAsync();
            }
        }
    }
}
