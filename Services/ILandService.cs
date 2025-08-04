using ViltrapportenApi.Data.CustomModels;
using ViltrapportenApi.Data.MapModels;
using ViltrapportenApi.Data.SystemModels;
using ViltrapportenApi.Modal;

namespace ViltrapportenApi.Services
{
    public interface ILandService
    {
        Task<Result<LandInformation>> GetSelectedLandServiceAsync(int landId);
        Task<Result<List<LandInformation>>> GetSelectedLandsServiceAsync(string municipality, int mainNo, int subNo, int? plotNo);
        Task<LandInfo> GetLandAsync(int landId);
        Task<List<LandOwnerInfo>> GetFilteredUserListAsync(string filter, int userDnnId, int isAdmin);
        Task<Result<LandObj>> GetLandDetailsAsync(int unitId, bool checkUnitType);
        Task<Result<LandOwnersObj>> GetLandOwnersDetailsAsync(int unitId);
        Task<Result<LandObj>> GetOwnersLandDetailsUnderUnitAsync(int ownerUId, bool isDnnId, int unitId, bool isLandTab);
        Task<Result<LandObj>> GetSharedOwnersLandDetailsUnderUnitAsync(string landIdListStr, int unitId);
        Task<Result<LandObj>> ArchiveLandAsync(ArchiveLand archiveLand);
        Task<int> ManageLandOwnerDetails(OwnerDetail ownerDetail);
        Task<LandOwnerRegisterDetail> GetLandOwnerDetailByLandIdAsync(int landId);
        Task<LandOwnerMapped> GetLandOwnerDetailsBySysUIdAsync(int systemUserId, int landId);
        #region manage land(save land)
        //Task<int> CreateLandAsync(string municipality, string mainNo, string subNo, string plotNo, int ownershipTypeId, float areaInForest, float areaInMountain, float areaInAgriculture, float totalArea, string notes, int createdBy);
        //Task<int> UpdateLandDetailsAsync(int id, string municipality, string mainNo, string subNo, string plotNo, int ownershipTypeId, float areaInForest, float areaInMountain, float areaInAgriculture, float totalArea, string notes, int createdBy);
        //Task AddLandUnitsAsync(int landId, int unitId, int landTypeId);
        //Task ManageLandOwnerAsync(int landId, int ownerId, int createdBy);
        //Task ArchiveLandOwnerAsync(int landId, int ownerId);
        Task<Result<MultipleLandOwner>> ManageLandDetails(LandDetail landDetail);
        #endregion manage land(save land)
    }
}
