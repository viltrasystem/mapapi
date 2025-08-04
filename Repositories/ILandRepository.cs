using Microsoft.AspNetCore.Http.HttpResults;
using ViltrapportenApi.Data.CustomModels;
using ViltrapportenApi.Data.SystemModels;
using ViltrapportenApi.Modal;

namespace ViltrapportenApi.Repositories
{
    public interface ILandRepository
    {
        Task<(List<LandMapped>, List<LandOwnerMapped>)> GetLandAndOwnersAsync(int unitId);
        Task<(List<LandMapped>, List<LandOwnerMapped>)> GetOwnersLandDetailsUnderUnitAsync(int landOwnerSysUid, int unitId, bool isDnnId);
        Task<(List<LandMapped>, List<LandOwnerMapped>)> GetSharedOwnersLandDetailsUnderUnitAsync(string landIdStr, int unitId);
        Task<(List<LandOwnerMapped> singleLandOwners, List<LandOwnerMapped> multipleLandOwners, List<LandOwnerMapped> GetLandOwnersDetails)> GetLandOwnersDetailsAsync(int unitId);
        Task<int> ArchiveLandAsync(int landId, int updatedBy);
        Task<List<OwnersState>> GetLandOwnerDetailByLandIdAsync(int landId);
        Task<LandOwnerMapped> GetLandOwnerDetailsBySysUIdAsync(int systemUserId, int landId);

        Task ManageLandOwnerAsync(int landId, int ownerId, int createdBy);
        Task ArchiveLandOwnerAsync(int landId, int ownerId);
        Task<List<LandOwnerMapped>> GetLandMultipleOwnersContactOwnerDetailsAsync();
        Task<int> ManageLandOwnerDetails(OwnerDetail ownerDetail);
        Task<List<LandOwnerInfo>> GetFilteredUserListAsync(string filter, int userDnnId, int isAdmin);
    }
}