using System.Collections.Generic;
using ViltrapportenApi.Data.CustomModels;
using ViltrapportenApi.Repositories;

namespace ViltrapportenApi.Services
{
    public class MainService(MainRepository mainRepository) : IMainService
    {
        //private readonly MainRepository _mainRepository;
        //public MainService(MainRepository mainRepository) {
        //    _mainRepository = mainRepository;
        //}

        public async Task<IList<UserUnit>> GetUserUnitAsync(int dnnUserId, int isAdmin)
        {
           return await mainRepository.GetUserUnitsAsync(dnnUserId, isAdmin);
        } 
        
        public async Task<IList<ChildUnit>> GetChildNodesAsync(int parentId, bool isUserOnlyOnMunicipality, bool isGuest)
        {
            IList<ChildUnit> childNodeList = new List<ChildUnit>();
          var childNodes =   await mainRepository.GetChildNodesAsync(parentId, isUserOnlyOnMunicipality, isGuest);
            foreach (var item in childNodes)
            {
                ChildUnit childNode = new ChildUnit()
                {
                    UnitID = item.UnitID,
                    Unit = item.Unit,
                    ParentID=parentId,
                    ChildCount= item.unitCount,
                    ImgUrl = item.ImgUrl,
                    IsActiveForHunting = item.IsActiveForHunting,
                    IsAllowedToRegisterLands = item.IsAllowedToRegisterLands,
                    IsArchived = item.IsArchived,
                    IsHuntingComplete = item.IsHuntingComplete,
                    ReferenceID = item.ReferenceID,
                    unitCount = item.unitCount,
                    UnitTypeID = item.UnitTypeID,
                    IsUserOnlyOnMunicipality = isUserOnlyOnMunicipality,
                    IsGuest = isGuest,
                    IsExpanded=false
                };

                childNodeList.Add(childNode);
            }

            return childNodeList;
        }
    }
}
