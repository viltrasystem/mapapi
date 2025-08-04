using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViltrapportenApi.Data.CustomModels;
using ViltrapportenApi.Modal;
using ViltrapportenApi.Services;

namespace ViltrapportenApi.Controllers
{
    [Authorize]
    public class MainController(IMainService mainService) : BaseApiController
    {
        [HttpGet(ApiRoutes.Main.UserUnit)]
        public async Task<ActionResult<IList<UserUnit>>> GetUserUnits(int dnnUserId, bool isAdmin)
        {
            int adminStatus = isAdmin ? 1 : 0;
            var userUnits = await mainService.GetUserUnitAsync(dnnUserId, adminStatus);
            return Ok(userUnits);   
        }
        
        [HttpGet(ApiRoutes.Main.ChildNode)]
        public async Task<ActionResult<IList<ChildUnit>>> GetChildNodes(int parentId, bool isUserOnlyOnMunicipality, bool isGuest)
        {
            var childUnits = await mainService.GetChildNodesAsync(parentId, isUserOnlyOnMunicipality, isGuest);
            return Ok(childUnits);
        }


        // private readonly IMainService _mainService;
        //public MainController(IMainService mainService)
        //{

        //}
    }
}
