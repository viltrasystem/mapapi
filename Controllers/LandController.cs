using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViltrapportenApi.Data.CustomModels;
using ViltrapportenApi.Data.SystemModels;
using ViltrapportenApi.Errors;
using ViltrapportenApi.Modal;
using ViltrapportenApi.Services;

namespace ViltrapportenApi.Controllers
{
    [Authorize]
    public class LandController(IDataService dataService, ILandService landService) : BaseApiController
    {
        [HttpGet(ApiRoutes.LandRoute.Municipality)]
        public async Task<ActionResult<IList<Municipality>>> Municipality()
        {
            var municipalityList = await dataService.GetMunicipalityAsync();

            return Ok(municipalityList);
        }


        [HttpGet(ApiRoutes.LandRoute.Land)]
        public async Task<ActionResult<LandInfo>> GetLand(int landId)
        {
            var land = await landService.GetLandAsync(landId);
            return Ok(land);
        }

        [HttpPost(ApiRoutes.LandRoute.Land)]
        public async Task<ActionResult<ApiResponse<MultipleLandOwner>>> ManageLandDetails(LandDetail land)
        {
            var landResult = await landService.ManageLandDetails(land);

            if (landResult.IsSuccess)
            {
                return Ok(new ApiResponse<MultipleLandOwner>(landResult.StatusCode, landResult.Value, landResult.Message));
            }
            else if (landResult.StatusCode == 404)
            {
                return NotFound(new ApiResponse<MultipleLandOwner>(landResult.StatusCode, null, landResult.Message));
            }
            else if (landResult.StatusCode == 400)
            {
                return BadRequest(new ApiResponse<MultipleLandOwner>(landResult.StatusCode, null, landResult.Message));
            }
            else
            {
                return landResult.StatusCode switch
                {
                    404 => NotFound(new ApiResponse<MultipleLandOwner>(landResult.StatusCode, null, landResult.Message)),
                    400 => BadRequest(new ApiResponse<MultipleLandOwner>(landResult.StatusCode, null, landResult.Message)),
                    _ => StatusCode(landResult.StatusCode, new ApiResponse<MultipleLandOwner>(landResult.StatusCode, null, landResult.Message))
                };
            }
        }

        [HttpGet(ApiRoutes.LandRoute.FilteredUser)]
        public async Task<ActionResult<List<Landowner>>> GetFilteredUserList(string filter, int userDnnId, int isAdmin)
        {
            IList<Landowner> landOwnerList = null;
            var land = await landService.GetFilteredUserListAsync(filter, userDnnId, isAdmin);
            if (land != null)
            {
                landOwnerList = land.Select(x => new Landowner()
                {
                    Id = x.SystemUserId,
                    Name = x.FullName
                }).ToList();
            }
            return Ok(landOwnerList);
        }

        [HttpGet(ApiRoutes.LandRoute.LandDetail)]
        public async Task<ActionResult<ApiResponse<LandObj>>> GetLandDetails(int unitId)
        {
            var landInfoResult = await landService.GetLandDetailsAsync(unitId, false);
            if (landInfoResult.IsSuccess)
            {
                if (landInfoResult.Value != null)
                {
                    return Ok(new ApiResponse<LandObj>(landInfoResult.StatusCode, landInfoResult.Value, landInfoResult.Message));
                }
                else
                {
                    return Ok(new ApiResponse<LandObj>(landInfoResult.StatusCode, null, landInfoResult.Message));
                }
            }
            else
            {
                return BadRequest(new ApiResponse<LandObj>(landInfoResult.StatusCode, null, landInfoResult.Message));
            }
        }

        [HttpGet(ApiRoutes.LandRoute.LandOwners)]
        public async Task<ActionResult<ApiResponse<LandOwnersObj>>> GetLandOwnersDetails(int unitId)
        {
            var landOwnersInfoResult = await landService.GetLandOwnersDetailsAsync(unitId);

            if (landOwnersInfoResult.IsSuccess)
            {
                if (landOwnersInfoResult.Value != null)
                {
                    return Ok(new ApiResponse<LandOwnersObj>(landOwnersInfoResult.StatusCode, landOwnersInfoResult.Value, landOwnersInfoResult.Message));
                }
                else
                {
                    return Ok(new ApiResponse<LandOwnersObj>(landOwnersInfoResult.StatusCode, null, landOwnersInfoResult.Message));
                }
            }
            else
            {
                return BadRequest(new ApiResponse<LandOwnersObj>(landOwnersInfoResult.StatusCode, null, landOwnersInfoResult.Message));
            }
        }

        [HttpGet(ApiRoutes.LandRoute.OwnersLand)]
        public async Task<ActionResult<ApiResponse<LandObj>>> GetOwnersLandDetailsUnderUnit(int unitId, int ownerUId, bool isDnnId, bool isLandTab)
        {
            var ownersLandInfoResult = await landService.GetOwnersLandDetailsUnderUnitAsync(ownerUId, isDnnId, unitId, isLandTab);
            if (ownersLandInfoResult.IsSuccess)
            {
                if (ownersLandInfoResult.Value != null)
                {
                    return Ok(new ApiResponse<LandObj>(ownersLandInfoResult.StatusCode, ownersLandInfoResult.Value, ownersLandInfoResult.Message));
                }
                else
                {
                    return Ok(new ApiResponse<LandObj>(ownersLandInfoResult.StatusCode, null, ownersLandInfoResult.Message));
                }
            }
            else
            {
                return BadRequest(new ApiResponse<LandObj>(ownersLandInfoResult.StatusCode, null, ownersLandInfoResult.Message));
            }
        }

        [HttpGet(ApiRoutes.LandRoute.SharedOwnersLand)]
        public async Task<ActionResult<IList<LandObj>>> GetSharedOwnersLandDetailsUnderUnitAsync(string landIdListStr, int unitId)
        {
            var sharedOwnersInfoResult = await landService.GetSharedOwnersLandDetailsUnderUnitAsync(landIdListStr, unitId);
            if (sharedOwnersInfoResult.IsSuccess)
            {
                if (sharedOwnersInfoResult.Value != null)
                {
                    return Ok(new ApiResponse<LandObj>(sharedOwnersInfoResult.StatusCode, sharedOwnersInfoResult.Value, sharedOwnersInfoResult.Message));
                }
                else
                {
                    return Ok(new ApiResponse<LandObj>(sharedOwnersInfoResult.StatusCode, null, sharedOwnersInfoResult.Message));
                }
            }
            else
            {
                return BadRequest(new ApiResponse<LandObj>(sharedOwnersInfoResult.StatusCode, null, sharedOwnersInfoResult.Message));
            }
        }

        [HttpPost(ApiRoutes.LandRoute.ArchiveLand)]
        public async Task<IActionResult> ArchiveLand(ArchiveLand archiveLand)
        {
            var landResult = await landService.ArchiveLandAsync(archiveLand);

            if (landResult.IsSuccess)
            {
                if (landResult.Value != null)
                {
                    return Ok(new ApiResponse<LandObj>(landResult.StatusCode, landResult.Value, landResult.Message));
                }
                else
                {
                    return Ok(new ApiResponse<LandObj>(landResult.StatusCode, null, landResult.Message));
                }
            }
            else
            {
                return BadRequest(new ApiResponse<LandObj>(landResult.StatusCode, null, landResult.Message));
            }
        }

        [HttpGet(ApiRoutes.LandRoute.Owners)]
        public async Task<ActionResult<IList<LandObj>>> GetLandOwnerDetailByLandId(int systemUserId, int landId)
        {
            var landOwnerRegisterDetail = new LandOwnerRegisterDetail();
            if (landId != 0)
            {
                var ownerDetail = await landService.GetLandOwnerDetailByLandIdAsync(landId);
                landOwnerRegisterDetail = new LandOwnerRegisterDetail()
                {
                    LandId = ownerDetail.LandId,
                    OwnersStates = ownerDetail.OwnersStates,
                    SystemUserId = ownerDetail.SystemUserId,
                    FullName = $"{ownerDetail.LastName}, {ownerDetail.FirstName}",
                    Email = ownerDetail.Email,
                    FirstName = ownerDetail.FirstName,
                    LastName = ownerDetail.LastName,
                    ContactNumber = ownerDetail.ContactNumber,
                    AddressCity = ownerDetail.AddressCity,
                    AddressLine1 = ownerDetail.AddressLine1,
                    AddressLine2 = ownerDetail.AddressLine2,
                    BankAccountNo = ownerDetail.BankAccountNo,
                    Notes = ownerDetail.Notes,
                };
            }
            else
            {
                var ownerDetail = await landService.GetLandOwnerDetailsBySysUIdAsync(systemUserId, landId);
                landOwnerRegisterDetail = new LandOwnerRegisterDetail()
                {
                    LandId = landId,
                    OwnersStates = null,
                    SystemUserId = ownerDetail.SystemUserId,
                    FullName = $"{ownerDetail.LastName}, {ownerDetail.FirstName}",
                    Email = ownerDetail.Email,
                    FirstName = ownerDetail.FirstName,
                    LastName = ownerDetail.LastName,
                    ContactNumber = ownerDetail.ContactNumber,
                    AddressCity = ownerDetail.AddressCity,
                    AddressLine1 = ownerDetail.AddressLine1,
                    AddressLine2 = ownerDetail.AddressLine2,
                    BankAccountNo = ownerDetail.BankAccountNo,
                    Notes = ownerDetail.Notes,
                };
            }

            return Ok(landOwnerRegisterDetail);
        }

        [HttpGet(ApiRoutes.LandRoute.OwnerDetail)]
        public async Task<ActionResult<IList<LandObj>>> GetLandOwnerDetail(int systemUserId, int landId)
        {
            var landOwnerRegisterDetail = new LandOwnerRegisterDetail();

            var ownerDetail = await landService.GetLandOwnerDetailsBySysUIdAsync(systemUserId, landId);
            if (ownerDetail != null)
            {
                landOwnerRegisterDetail = new LandOwnerRegisterDetail()
                {
                    LandId = landId,
                    OwnersStates = null,
                    SystemUserId = ownerDetail.SystemUserId,
                    FullName = $"{ownerDetail.LastName}, {ownerDetail.FirstName}",
                    Email = ownerDetail.Email,
                    FirstName = ownerDetail.FirstName,
                    LastName = ownerDetail.LastName,
                    ContactNumber = ownerDetail.ContactNumber,
                    AddressCity = ownerDetail.AddressCity,
                    AddressLine1 = ownerDetail.AddressLine1,
                    AddressLine2 = ownerDetail.AddressLine2,
                    BankAccountNo = ownerDetail.BankAccountNo,
                    Notes = ownerDetail.Notes,
                };
            }


            return Ok(landOwnerRegisterDetail);
        }

        [HttpPost(ApiRoutes.LandRoute.OwnerDetail)]
        public async Task<ActionResult<int>> ManageLandOwnerDetails(OwnerDetail ownerDetail)
        {
            var id = await landService.ManageLandOwnerDetails(ownerDetail);

            return Ok(id);
        }

    }
}
