using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Transactions;
using ViltrapportenApi.Data.CustomModels;
using ViltrapportenApi.Data.MapModels;
using ViltrapportenApi.Data.SystemModels;
using ViltrapportenApi.Middleware;
using ViltrapportenApi.Modal;
using ViltrapportenApi.Repositories;
using static ViltrapportenApi.Modal.ApiRoutes;

namespace ViltrapportenApi.Services
{
    public class LandService(ViltrapportenMapContext mapContext, ViltrapportenSystemContext systemContext, ILandRepository landRepository) : ILandService
    {
        public async Task<Result<LandInformation>> GetSelectedLandServiceAsync(int landId)
        {

            // Step 1: Fetch active lands
            var land = await systemContext.Lands
                .Where(x => x.LandId == landId && x.IsActive)
                .FirstOrDefaultAsync();

            if (land == null)
            {
                return Result<LandInformation>.Failure("No matching land found.", 404);
            }

            var landOwnerUserList = await systemContext.LandOwners
                .Where(x => x.IsActive && x.LandId == landId)
                .Join(
                    systemContext.ViltraUsers,
                    o => o.SystemUserId,
                    v => v.UserId,
                    (o, v) => new
                    {
                        LandId = o.LandId,
                        SystemUserId = o.SystemUserId,
                        FirstName = v.FirstName,
                        LastName = v.LastName,
                        ContactNo = v.ContactNumber,
                        Email = v.Email
                    })
                .ToListAsync();

            var groupedOwnerUsers = landOwnerUserList
                .GroupBy(x => new { x.LandId, x.SystemUserId, x.FirstName, x.LastName, x.ContactNo, x.Email })
                .ToList();

            var landUnitList = await systemContext.LandUnits
                    .Where(x => x.LandId == land.LandId)
                    .Join(
                        systemContext.Units.Where(x => x.IsActive),
                        o => o.UnitId,
                        u => u.UnitId,
                        (o, u) => new LandUnitInfo
                        {
                            UnitId = o.UnitId,
                            LandTypeId = o.LandTypeId,
                            Unit = u.Unit1,
                            UnitTypeId = u.TypeId
                        })
                    .ToListAsync();

            var result = new LandInformation
            {
                LandId = land.LandId,
                AreaInAgriculture = land.AreaInAgriculture,
                AreaInForest = land.AreaInForest,
                AreaInMountain = land.AreaInMountain,
                TotalArea = land.TotalArea,
                Municipality = land.Municipality,
                MainNo = land.MainNo,
                SubNo = land.SubNo,
                PlotNo = land.PlotNo,
                Notes = land.Notes,
                LandOwners = groupedOwnerUsers.Select(x => new LandOwnerInformation
                {
                    LandId = x.Key.LandId,
                    SystemUserId = x.Key.SystemUserId,
                    OwnerName = $"{x.Key.FirstName} {x.Key.LastName}",
                    ContactNo = x.Key.ContactNo,
                    Email = x.Key.Email
                }).ToList(),
                LandUnits = landUnitList
            };

            return Result<LandInformation>.Success(result, null, 200);
        }

        public async Task<Result<List<LandInformation>>> GetSelectedLandsServiceAsync(string municipality, int mainNo, int subNo, int? plotNo)
        {

            // Step 1: Fetch active lands
            var lands = await systemContext.Lands
                .Where(x => x.Municipality == municipality && x.MainNo == mainNo.ToString() && x.SubNo == subNo.ToString() && (plotNo != null ? x.PlotNo == plotNo.ToString() : true) && x.IsActive).ToListAsync();

            if (lands == null || !lands.Any())
            {
                return Result<List<LandInformation>>.Success(null, "No matching land found.", 204);
            }
            var landInformations = new List<LandInformation>();
            foreach (var land in lands)
            {
                var landOwnerUserList = await systemContext.LandOwners
                    .Where(x => x.IsActive && x.LandId == land.LandId)
                    .Join(
                        systemContext.ViltraUsers,
                        o => o.SystemUserId,
                        v => v.UserId,
                        (o, v) => new
                        {
                            o.LandId,
                            o.SystemUserId,
                            v.FirstName,
                            v.LastName,
                            ContactNo = v.ContactNumber,
                            v.Email
                        })
                    .ToListAsync();

                var groupedOwnerUsers = landOwnerUserList
                    .GroupBy(x => new { x.LandId, x.SystemUserId, x.FirstName, x.LastName, x.ContactNo, x.Email })
                    .ToList();

                var landUnitList = await systemContext.LandUnits
                    .Where(x => x.LandId == land.LandId)
                    .Join(
                        systemContext.Units.Where(x => x.IsActive),
                        o => o.UnitId,
                        u => u.UnitId,
                        (o, u) => new LandUnitInfo
                        {
                            UnitId = o.UnitId,
                            LandTypeId = o.LandTypeId,
                            Unit = u.Unit1,
                            UnitTypeId = u.TypeId
                        })
                    .ToListAsync();

                var landInfo = new LandInformation
                {
                    LandId = land.LandId,
                    AreaInAgriculture = land.AreaInAgriculture,
                    AreaInForest = land.AreaInForest,
                    AreaInMountain = land.AreaInMountain,
                    TotalArea = land.TotalArea,
                    Municipality = land.Municipality,
                    MainNo = land.MainNo,
                    SubNo = land.SubNo,
                    PlotNo = land.PlotNo,
                    Notes = land.Notes,
                    LandOwners = groupedOwnerUsers.Select(x => new LandOwnerInformation
                    {
                        LandId = x.Key.LandId,
                        SystemUserId = x.Key.SystemUserId,
                        OwnerName = $"{x.Key.FirstName} {x.Key.LastName}",
                        ContactNo = x.Key.ContactNo,
                        Email = x.Key.Email
                    }).ToList(),
                    LandUnits = landUnitList
                };
                landInformations.Add(landInfo);
            }
            return Result<List<LandInformation>>.Success(landInformations, null, 200); ;
        }

        public async Task<LandInfo> GetLandAsync(int landId)
        {
            LandInfo land = null;
            var landownershipTypes = await systemContext.LandownershipTypes.Where(x => x.IsActive).Select(x => new LandOwnershipType()
            {
                Id = x.OwnershipTypeId,
                OwnershipType = x.OwnershipType
            }).ToListAsync();
            if (landId > 0)
            {

                var result = await (from l in systemContext.Lands
                                    join lo in systemContext.LandOwners on l.LandId equals lo.LandId
                                    join us in systemContext.ViltraUsers on lo.SystemUserId equals us.UserId
                                    join lu in systemContext.LandUnits on l.LandId equals lu.LandId
                                    join u in systemContext.Units on lu.UnitId equals u.UnitId
                                    where lo.IsActive && l.LandId == landId
                                    select new
                                    {
                                        l.LandId,
                                        l.Municipality,
                                        l.MainNo,
                                        l.SubNo,
                                        l.PlotNo,
                                        l.OwnershipTypeId,
                                        l.AreaInForest,
                                        l.AreaInMountain,
                                        l.AreaInAgriculture,
                                        l.Notes,
                                        lo.SystemUserId,
                                        lu.UnitId,
                                        Unit = u.Unit1,
                                        lu.LandTypeId,
                                        FullName = $"{us.FirstName} {us.LastName}"
                                    }).ToListAsync();

                if (result != null)
                {
                    var landObj = result.FirstOrDefault();
                    land = new LandInfo
                    {
                        LandId = landObj.LandId,
                        Municipality = landObj.Municipality,
                        MainNo = landObj.MainNo,
                        SubNo = landObj.SubNo,
                        PlotNo = landObj.PlotNo,
                        OwnershipTypeId = landObj.OwnershipTypeId,
                        AreaInForest = landObj.AreaInForest,
                        AreaInMountain = landObj.AreaInMountain,
                        AreaInAgriculture = landObj.AreaInAgriculture,
                        Notes = landObj.Notes,
                        LandOwners = (from t in result
                                      group t by new { ownerId = t.SystemUserId, fullName = t.FullName } into grp
                                      select new LandOwnerInfo
                                      {
                                          LandOwnerId = grp.Key.ownerId,
                                          FullName = grp.Key.fullName.Trim()
                                      }).ToList(),
                        LandUnits = (from t in result
                                     group t by new
                                     {
                                         unitId = t.UnitId,
                                         unit = t.Unit,
                                         landTypeId = t.LandTypeId
                                     } into grp
                                     select new LandUnitInfo
                                     {
                                         UnitId = grp.Key.unitId,
                                         Unit = grp.Key.unit.Trim(),
                                         LandTypeId = grp.Key.landTypeId
                                     }).ToList(),
                        LandOwnershipTypes = landownershipTypes
                    };
                }
            }
            else
            {
                land = new LandInfo()
                {
                    LandOwnershipTypes = landownershipTypes
                };
            }
            return land;
        }

        public async Task<List<LandOwnerInfo>> GetFilteredUserListAsync(string filter, int userDnnId, int isAdmin)
        {
            return await landRepository.GetFilteredUserListAsync(filter, userDnnId, isAdmin);
        }

        public async Task<Result<LandObj>> GetLandDetailsAsync(int unitId, bool checkUnitType)
        {
            var landObj = new LandObj();

            try
            {
                if (checkUnitType)
                {
                    var parentId = await systemContext.Units
                        .Where(x => x.UnitId == unitId && x.IsActive && x.TypeId == 6)
                        .Join(
                            systemContext.Units,
                            child => child.ParentId,
                            parent => parent.UnitId,
                            (child, parent) => parent.UnitId
                        )
                        .FirstOrDefaultAsync();

                    if (parentId != 0)
                    {
                        unitId = parentId;
                    }
                }

                var (lands, landOwners) = await landRepository.GetLandAndOwnersAsync(unitId);

                if (lands == null || !lands.Any())
                {
                    return Result<LandObj>.Success(null, "No land details in current unit", 204);
                }

                var landTable = lands.AsEnumerable();

                var _base = (from l in landTable
                             group l by new
                             {
                                 l.LandId,
                                 l.Municipality,
                                 l.MainNo,
                                 l.SubNo
                             } into grp
                             select new
                             {
                                 grp.Key.LandId,
                                 grp.Key.Municipality,
                                 grp.Key.MainNo,
                                 grp.Key.SubNo
                             });

                var group_list = (from l in _base
                                  group l by new
                                  {
                                      l.Municipality,
                                      l.MainNo,
                                      l.SubNo
                                  } into grp
                                  select new
                                  {
                                      grp.Key.Municipality,
                                      grp.Key.MainNo,
                                      grp.Key.SubNo,
                                      NoOfOccurrences = grp.Count()
                                  });

                landObj.Lands = (from l in landTable
                                 group l by new
                                 {
                                     l.LandId,
                                     l.Municipality,
                                     l.MainNo,
                                     l.SubNo,
                                     l.PlotNo,
                                     l.AreaInForest,
                                     l.AreaInAgriculture,
                                     l.AreaInMountain,
                                     l.TotalArea,
                                     l.Notes,
                                     l.OwnershipTypeId,
                                     l.OwnershipType
                                 }
                                 into grp
                                 join g in group_list
                                 on new
                                 {
                                     grp.Key.Municipality,
                                     grp.Key.MainNo,
                                     grp.Key.SubNo
                                 }
                                 equals new { g.Municipality, g.MainNo, g.SubNo }
                                 select new LandInfo
                                 {
                                     LandId = grp.Key.LandId,
                                     Municipality = grp.Key.Municipality,
                                     MainNo = grp.Key.MainNo,
                                     SubNo = grp.Key.SubNo,
                                     PlotNo = grp.Key.PlotNo,
                                     AreaInForest = grp.Key.AreaInForest,
                                     AreaInAgriculture = grp.Key.AreaInAgriculture,
                                     AreaInMountain = grp.Key.AreaInMountain,
                                     TotalArea = grp.Key.TotalArea,
                                     Notes = grp.Key.Notes,
                                     OwnershipTypeId = grp.Key.OwnershipTypeId,
                                     OwnershipType = grp.Key.OwnershipType,
                                     NoOfReferencedLands = g.NoOfOccurrences,
                                     LandOwners = (from o in landTable
                                                   where o.LandId == grp.Key.LandId
                                                   group o by new
                                                   {
                                                       o.LandOwnerId,
                                                       o.SystemUserId,
                                                       o.FirstName,
                                                       o.LastName,
                                                       o.Email,
                                                       o.ContactNumber,
                                                       o.ContactOwnerLandId
                                                   } into ogrp
                                                   select new LandOwnerInfo
                                                   {
                                                       LandId = grp.Key.LandId,
                                                       LandOwnerId = ogrp.Key.LandOwnerId,
                                                       SystemUserId = ogrp.Key.SystemUserId,
                                                       FullName = ogrp.Key.FirstName + " " + ogrp.Key.LastName,
                                                       Email = ogrp.Key.Email,
                                                       ContactNumber = ogrp.Key.ContactNumber,
                                                       ContactOwnerLandId = ogrp.Key.ContactOwnerLandId
                                                   }).ToList(),
                                     LandUnits = (from u in landTable
                                                  where u.LandId == grp.Key.LandId
                                                  group u by new
                                                  {
                                                      u.LandId,
                                                      u.UnitId,
                                                      Unit = "[ " + u.ParentUnit + " ] " + u.Unit,
                                                      // UnitTypeId = u.type,
                                                      u.LandTypeId
                                                  } into ugrp
                                                  select new LandUnitInfo
                                                  {
                                                      UnitId = ugrp.Key.UnitId,
                                                      Unit = ugrp.Key.Unit,
                                                      LandTypeId = ugrp.Key.LandTypeId,
                                                      // UnitTypeId = 
                                                  }).ToList()
                                 }).OrderBy(x => x.Municipality.Trim())
                                   .ThenBy(x => x.MainNo.Trim())
                                   .ThenBy(x => x.SubNo.Trim())
                                   .ThenBy(x => x.PlotNo.Trim())
                                   .ToList();

                #region multipleLandMultipleSameOwnerShip

                var allSharedLandDt = landOwners.AsEnumerable();

                var AllSharedLandOwnerList = (from t in allSharedLandDt
                                              group t by new
                                              {
                                                  t.LandId
                                              } into grp
                                              select new OwnerInfo
                                              {
                                                  LandId = grp.Key.LandId,
                                                  Owners = (from lo in allSharedLandDt
                                                            where lo.LandId == grp.Key.LandId
                                                            group lo by new
                                                            {
                                                                lo.SystemUserId,
                                                                lo.FullName,
                                                                lo.DisplayName,
                                                                lo.Email,
                                                                lo.ContactNumber,
                                                                lo.LandOwnerId,
                                                                lo.ContactOwnerLandId,
                                                                lo.AddressLine1,
                                                                lo.AddressLine2,
                                                                lo.AddressCity,
                                                                lo.BankAccountNo,
                                                                lo.Notes,
                                                            } into g
                                                            select new OwnerInfo
                                                            {
                                                                LandId = grp.Key.LandId,
                                                                SystemUserId = g.Key.SystemUserId,
                                                                FullName = g.Key.FullName,
                                                                DisplayName = g.Key.DisplayName,
                                                                Email = g.Key.Email,
                                                                ContactNumber = g.Key.ContactNumber,
                                                                LandOwnerId = g.Key.LandOwnerId,
                                                                //ContactOwnerLandId = g.Key.ContactOwnerLandId,
                                                                AddLine1 = g.Key.AddressLine1,
                                                                AddLine2 = g.Key.AddressLine2,
                                                                AddCity = g.Key.AddressCity,
                                                                BankAccountNo = g.Key.BankAccountNo,
                                                                Notes = g.Key.Notes,
                                                            }).ToList()
                                              }).ToList();

                ManageMultipleLandMultipleSameOwnerShip(AllSharedLandOwnerList, ref landObj);

                #endregion

                landObj.TotalLandsCount = landObj.Lands.Count();
                landObj.TotalSharedLandsCount = group_list.Sum(x => x.NoOfOccurrences);
                landObj.TotalForestArea = landObj.Lands.Sum(x => x.AreaInForest);
                landObj.TotalAgricultureArea = landObj.Lands.Sum(x => x.AreaInAgriculture);
                landObj.TotalMountainArea = landObj.Lands.Sum(x => x.AreaInMountain);
                landObj.TotalArea = landObj.Lands.Sum(x => x.TotalArea);

                return Result<LandObj>.Success(landObj, null, 200);
            }
            catch (Exception e)
            {
                return Result<LandObj>.Failure($"An error occurred while retrieving land details: {e.Message}", 500);
            }
        }

        /// <summary>
        /// Retrieves given Owners Land Details Under the given Unit
        /// </summary>
        /// <param name="ownerUId">Owner system id</param>
        /// <param name="isDnnId">is given id dnn id?</param>
        /// <param name="unitId">Unit which request load owners land data</param>
        /// <param name="isLandTab">is request from land tab(false), from owners tab</param>
        /// <returns>A user specific land data</returns>
        /// <remarks>
        /// 
        /// 
        /// </remarks>
        public async Task<Result<LandObj>> GetOwnersLandDetailsUnderUnitAsync(int ownerUId, bool isDnnId, int unitId, bool isLandTab)
        {
            var landObj = new LandObj();

            try
            {
                var (landMappedList, landOwnerMappedList) = await landRepository.GetOwnersLandDetailsUnderUnitAsync(ownerUId, unitId, isDnnId);

                if (!landMappedList.Any())
                {
                    return Result<LandObj>.Success(null, "No land details for current user in the given uint", 204);
                }

                var landUserList = landMappedList
                    .Select(l => new
                    {
                        l.LandId,
                        l.SystemUserId
                    })
                    .GroupBy(x => new { x.LandId, x.SystemUserId })
                    .Select(g => new
                    {
                        LandId = g.Key.LandId,
                        SystemUserId = g.Key.SystemUserId
                    })
                    .ToList();

                var landUserDic = new Dictionary<int, int>();
                var multipleOwnerLandList = new List<int>();
                foreach (var item in landUserList)
                {
                    if (landUserDic.ContainsKey(item.LandId))
                    {
                        if (landUserDic[item.LandId] != item.SystemUserId)
                        {
                            multipleOwnerLandList.Add(item.LandId);
                        }
                    }
                    else
                    {
                        landUserDic.Add(item.LandId, item.SystemUserId);
                    }
                }

                var _base = landMappedList
             .GroupBy(l => new
             {
                 l.LandId,
                 l.Municipality,
                 l.MainNo,
                 l.SubNo
             })
             .Select(grp => new
             {
                 grp.Key.LandId,
                 grp.Key.Municipality,
                 grp.Key.MainNo,
                 grp.Key.SubNo
             });

                var group_list = _base
                    .GroupBy(l => new
                    {
                        l.Municipality,
                        l.MainNo,
                        l.SubNo
                    })
                    .Select(grp => new
                    {
                        grp.Key.Municipality,
                        grp.Key.MainNo,
                        grp.Key.SubNo,
                        NoOfOccurrences = grp.Count()
                    });

                landObj.Lands = landMappedList.Where(l => !multipleOwnerLandList.Contains(l.LandId)).GroupBy(l => new
                {
                    l.LandId,
                    l.Municipality,
                    l.MainNo,
                    l.SubNo,
                    l.PlotNo,
                    l.AreaInForest,
                    l.AreaInAgriculture,
                    l.AreaInMountain,
                    l.TotalArea,
                    l.Notes,
                    l.OwnershipTypeId,
                    l.OwnershipType
                }).Join(group_list,
                        landGroup => new { landGroup.Key.Municipality, landGroup.Key.MainNo, landGroup.Key.SubNo },
                        groupedLand => new { groupedLand.Municipality, groupedLand.MainNo, groupedLand.SubNo },
                        (landGroup, groupedLand) => new LandInfo
                        {
                            LandId = landGroup.Key.LandId,
                            Municipality = landGroup.Key.Municipality,
                            MainNo = landGroup.Key.MainNo,
                            SubNo = landGroup.Key.SubNo,
                            PlotNo = landGroup.Key.PlotNo,
                            AreaInForest = landGroup.Key.AreaInForest,
                            AreaInAgriculture = landGroup.Key.AreaInAgriculture,
                            AreaInMountain = landGroup.Key.AreaInMountain,
                            TotalArea = landGroup.Key.TotalArea,
                            Notes = landGroup.Key.Notes,
                            OwnershipTypeId = landGroup.Key.OwnershipTypeId,
                            OwnershipType = landGroup.Key.OwnershipType,
                            NoOfReferencedLands = groupedLand.NoOfOccurrences,
                            LandOwners = landMappedList
                                .Where(o => o.LandId == landGroup.Key.LandId)
                                .GroupBy(o => new
                                {
                                    o.LandOwnerId,
                                    uid = (!isDnnId ? o.SystemUserId : o.DnnUserId),
                                    o.FirstName,
                                    o.LastName,
                                    o.Email,
                                    o.ContactNumber
                                })
                                .Select(ogrp => new LandOwnerInfo
                                {
                                    LandOwnerId = ogrp.Key.LandOwnerId,
                                    FullName = $"{ogrp.Key.FirstName} {ogrp.Key.LastName}",
                                    Email = ogrp.Key.Email,
                                    ContactNumber = ogrp.Key.ContactNumber,
                                    IsSelected = (ogrp.Key.uid == ownerUId)
                                })
                                .ToList(),
                            LandUnits = landMappedList
                                .Where(u => u.LandId == landGroup.Key.LandId)
                                .GroupBy(u => new
                                {
                                    u.LandId,
                                    u.UnitId,
                                    Unit = $"[ {u.ParentUnit} ] {u.Unit}",
                                    u.LandTypeId
                                })
                                .Select(uGrp => new LandUnitInfo
                                {
                                    UnitId = uGrp.Key.UnitId,
                                    Unit = uGrp.Key.Unit,
                                    LandTypeId = uGrp.Key.LandTypeId
                                })
                                .ToList()
                        })
                    .OrderBy(x => x.Municipality.Trim())
                    .ThenBy(x => x.MainNo.Trim())
                    .ThenBy(x => x.SubNo.Trim())
                    .ThenBy(x => x.PlotNo.Trim())
                    .ToList();

                if (isLandTab)
                {
                    var multiOwnerLand = landMappedList
                        .Where(l => multipleOwnerLandList.Contains(l.LandId))
                        .GroupBy(l => new
                        {
                            l.LandId,
                            l.Municipality,
                            l.MainNo,
                            l.SubNo,
                            l.PlotNo,
                            l.AreaInForest,
                            l.AreaInAgriculture,
                            l.AreaInMountain,
                            l.TotalArea,
                            l.Notes,
                            l.OwnershipTypeId,
                            l.OwnershipType
                        })
                        .Join(group_list,
                            landGroup => new { landGroup.Key.Municipality, landGroup.Key.MainNo, landGroup.Key.SubNo },
                            groupedLand => new { groupedLand.Municipality, groupedLand.MainNo, groupedLand.SubNo },
                            (landGroup, groupedLand) => new LandInfo
                            {
                                LandId = landGroup.Key.LandId,
                                Municipality = landGroup.Key.Municipality,
                                MainNo = landGroup.Key.MainNo,
                                SubNo = landGroup.Key.SubNo,
                                PlotNo = landGroup.Key.PlotNo,
                                AreaInForest = landGroup.Key.AreaInForest,
                                AreaInAgriculture = landGroup.Key.AreaInAgriculture,
                                AreaInMountain = landGroup.Key.AreaInMountain,
                                TotalArea = landGroup.Key.TotalArea,
                                Notes = landGroup.Key.Notes,
                                OwnershipTypeId = landGroup.Key.OwnershipTypeId,
                                OwnershipType = landGroup.Key.OwnershipType,
                                NoOfReferencedLands = groupedLand.NoOfOccurrences,
                                LandOwners = landOwnerMappedList
                                    .Where(o => o.LandId == landGroup.Key.LandId)
                                    .GroupBy(o => new
                                    {
                                        o.LandOwnerId,
                                        uid = (!isDnnId ? o.SystemUserId : o.DnnUserId),
                                        o.FirstName,
                                        o.LastName,
                                        o.Email,
                                        o.ContactNumber
                                    })
                                    .Select(ogrp => new LandOwnerInfo
                                    {
                                        LandOwnerId = ogrp.Key.LandOwnerId,
                                        FullName = $"{ogrp.Key.FirstName} {ogrp.Key.LastName}",
                                        Email = ogrp.Key.Email,
                                        ContactNumber = ogrp.Key.ContactNumber,
                                        IsSelected = (ogrp.Key.uid == ownerUId)
                                    })
                                    .ToList(),
                                LandUnits = landMappedList
                                    .Where(u => u.LandId == landGroup.Key.LandId)
                                    .GroupBy(u => new
                                    {
                                        u.LandId,
                                        u.UnitId,
                                        Unit = $"[ {u.ParentUnit} ] {u.Unit}",
                                        u.LandTypeId
                                    })
                                    .Select(uGrp => new LandUnitInfo
                                    {
                                        UnitId = uGrp.Key.UnitId,
                                        Unit = uGrp.Key.Unit,
                                        LandTypeId = uGrp.Key.LandTypeId
                                    })
                                    .ToList()
                            })
                        .OrderBy(x => x.Municipality.Trim())
                        .ThenBy(x => x.MainNo.Trim())
                        .ThenBy(x => x.SubNo.Trim())
                        .ThenBy(x => x.PlotNo.Trim())
                        .ToList();

                    var allSharedLandOwnerList = landOwnerMappedList
                        .GroupBy(o => o.LandId)
                        .Select(grp => new OwnerInfo
                        {
                            LandId = grp.Key,
                            Owners = landOwnerMappedList.Where(lo => lo.LandId == grp.Key)
                                .GroupBy(o => new
                                {
                                    o.SystemUserId,
                                    o.FullName,
                                    o.DisplayName,
                                    o.Email,
                                    o.ContactNumber,
                                    o.LandOwnerId,
                                    o.ContactOwnerLandId,
                                    o.AddressLine1,
                                    o.AddressLine2,
                                    o.AddressCity,
                                    o.BankAccountNo,
                                    o.Notes
                                })
                                .Select(g => new OwnerInfo
                                {
                                    LandId = grp.Key,
                                    SystemUserId = g.Key.SystemUserId,
                                    FullName = g.Key.FullName,
                                    DisplayName = g.Key.DisplayName,
                                    Email = g.Key.Email,
                                    ContactNumber = g.Key.ContactNumber,
                                    LandOwnerId = g.Key.LandOwnerId,
                                    ContactOwnerLandId = g.Key.ContactOwnerLandId,
                                    AddLine1 = g.Key.AddressLine1,
                                    AddLine2 = g.Key.AddressLine2,
                                    AddCity = g.Key.AddressCity,
                                    BankAccountNo = g.Key.BankAccountNo,
                                    Notes = g.Key.Notes
                                })
                                .ToList()
                        })
                        .ToList();

                    //landObj.SharedLands = multiOwnerLand;
                    //landObj.SharedLandOwners = allSharedLandOwnerList;
                    landObj.Lands.AddRange(multiOwnerLand);
                    ManageMultipleLandMultipleSameOwnerShip(allSharedLandOwnerList, ref landObj);
                }

                // Set other landObj properties if needed
                landObj.TotalLandsCount = landObj.Lands.Count;
                landObj.TotalSharedLandsCount = group_list.Sum(x => x.NoOfOccurrences);
                landObj.TotalForestArea = landObj.Lands.Sum(l => l.AreaInForest);
                landObj.TotalMountainArea = landObj.Lands.Sum(l => l.AreaInMountain);
                landObj.TotalAgricultureArea = landObj.Lands.Sum(l => l.AreaInAgriculture);
                landObj.TotalArea = landObj.Lands.Sum(l => l.TotalArea);

                return Result<LandObj>.Success(landObj, null, 200);
            }
            catch (Exception ex)
            {
                return Result<LandObj>.Failure($"An error occurred while retrieving land owner details: {ex.Message}", 500);
            }
        }

        public async Task<Result<LandObj>> GetSharedOwnersLandDetailsUnderUnitAsync(string landIdListStr, int unitId)
        {
            var landObj = new LandObj();

            try
            {
                var (landMappedList, landOwnerMappedList) = await landRepository.GetSharedOwnersLandDetailsUnderUnitAsync(landIdListStr, unitId);

                if (landMappedList == null || !landMappedList.Any() || landOwnerMappedList == null || !landOwnerMappedList.Any())
                {
                    return Result<LandObj>.Success(null, "No shared land details in the given unit", 204);
                }

                var _base = landMappedList
                    .GroupBy(l => new
                    {
                        l.LandId,
                        l.Municipality,
                        l.MainNo,
                        l.SubNo
                    })
                    .Select(grp => new
                    {
                        grp.Key.LandId,
                        grp.Key.Municipality,
                        grp.Key.MainNo,
                        grp.Key.SubNo
                    });

                var group_list = _base
                    .GroupBy(l => new
                    {
                        l.Municipality,
                        l.MainNo,
                        l.SubNo
                    })
                    .Select(grp => new
                    {
                        grp.Key.Municipality,
                        grp.Key.MainNo,
                        grp.Key.SubNo,
                        NoOfOccurrences = grp.Count()
                    });

                landObj.Lands = landMappedList.GroupBy(l => new
                {
                    l.LandId,
                    l.Municipality,
                    l.MainNo,
                    l.SubNo,
                    l.PlotNo,
                    l.AreaInForest,
                    l.AreaInAgriculture,
                    l.AreaInMountain,
                    l.TotalArea,
                    l.Notes,
                    l.OwnershipTypeId,
                    l.OwnershipType
                })
                .Join(group_list, tab => new { tab.Key.Municipality, tab.Key.MainNo, tab.Key.SubNo }, gl => new { gl.Municipality, gl.MainNo, gl.SubNo }, (tab, gl) => new { tab, gl })
                .Select(x => new LandInfo
                {
                    LandId = x.tab.Key.LandId,
                    Municipality = x.tab.Key.Municipality,
                    MainNo = x.tab.Key.MainNo,
                    SubNo = x.tab.Key.SubNo,
                    PlotNo = x.tab.Key.PlotNo,
                    AreaInForest = x.tab.Key.AreaInForest,
                    AreaInAgriculture = x.tab.Key.AreaInAgriculture,
                    AreaInMountain = x.tab.Key.AreaInMountain,
                    TotalArea = x.tab.Key.TotalArea,
                    Notes = x.tab.Key.Notes,
                    OwnershipTypeId = x.tab.Key.OwnershipTypeId,
                    OwnershipType = x.tab.Key.OwnershipType,
                    NoOfReferencedLands = x.gl.NoOfOccurrences,
                    LandOwners = landOwnerMappedList
                        .Where(o => o.LandId == x.tab.Key.LandId)
                        .GroupBy(o => new
                        {
                            o.LandOwnerId,
                            o.SystemUserId,
                            o.FirstName,
                            o.LastName,
                            o.Email,
                            o.ContactNumber,
                            o.ContactOwnerLandId
                        })
                        .Select(ogrp => new LandOwnerInfo
                        {
                            LandId = x.tab.Key.LandId,
                            LandOwnerId = ogrp.Key.LandOwnerId,
                            SystemUserId = ogrp.Key.SystemUserId,
                            FullName = $"{ogrp.Key.FirstName} {ogrp.Key.LastName}",
                            Email = ogrp.Key.Email,
                            ContactNumber = ogrp.Key.ContactNumber,
                            ContactOwnerLandId = ogrp.Key.ContactOwnerLandId,
                            IsSelected = false
                        })
                        .ToList(),
                    LandUnits = landMappedList
                        .Where(u => u.LandId == x.tab.Key.LandId)
                        .GroupBy(u => new
                        {
                            u.LandId,
                            u.UnitId,
                            u.ParentUnit,
                            u.Unit,
                            u.LandTypeId
                        })
                        .Select(ugrp => new LandUnitInfo
                        {
                            UnitId = ugrp.Key.UnitId,
                            Unit = $"[ {ugrp.Key.ParentUnit} ] {ugrp.Key.Unit}",
                            LandTypeId = ugrp.Key.LandTypeId
                        })
                        .ToList()
                })
                .OrderBy(x => x.Municipality.Trim())
                .ThenBy(x => x.MainNo.Trim())
                .ThenBy(x => x.SubNo.Trim())
                .ThenBy(x => x.PlotNo.Trim())
                .ToList();

                #region multipleLandMultipleSameOwnerShip

                var allSharedLandOwnerList = landOwnerMappedList
                    .GroupBy(t => t.LandId)
                    .Select(grp => new OwnerInfo
                    {
                        LandId = grp.Key,
                        Owners = landOwnerMappedList
                            .Where(lo => lo.LandId == grp.Key)
                            .GroupBy(lo => new
                            {
                                lo.SystemUserId,
                                lo.FullName,
                                lo.DisplayName,
                                lo.Email,
                                lo.ContactNumber,
                                lo.LandOwnerId,
                                lo.ContactOwnerLandId,
                                lo.AddressLine1,
                                lo.AddressLine2,
                                lo.AddressCity,
                                lo.BankAccountNo,
                                lo.Notes
                            })
                            .Select(g => new OwnerInfo
                            {
                                LandId = grp.Key,
                                SystemUserId = g.Key.SystemUserId,
                                FullName = g.Key.FullName,
                                DisplayName = g.Key.DisplayName,
                                Email = g.Key.Email,
                                ContactNumber = g.Key.ContactNumber,
                                LandOwnerId = g.Key.LandOwnerId,
                                ContactOwnerLandId = g.Key.ContactOwnerLandId,
                                AddLine1 = g.Key.AddressLine1,
                                AddLine2 = g.Key.AddressLine2,
                                AddCity = g.Key.AddressCity,
                                BankAccountNo = g.Key.BankAccountNo,
                                Notes = g.Key.Notes,
                            })
                            .ToList()
                    })
                    .ToList();

                ManageMultipleLandMultipleSameOwnerShip(allSharedLandOwnerList, ref landObj);

                #endregion

                // Update summary values
                landObj.TotalLandsCount = landObj.Lands.Count();
                landObj.TotalSharedLandsCount = group_list.Sum(x => x.NoOfOccurrences);
                landObj.TotalForestArea = landObj.Lands.Sum(x => x.AreaInForest);
                landObj.TotalAgricultureArea = landObj.Lands.Sum(x => x.AreaInAgriculture);
                landObj.TotalMountainArea = landObj.Lands.Sum(x => x.AreaInMountain);
                landObj.TotalArea = landObj.Lands.Sum(x => x.TotalArea);
            }
            catch (Exception ex)
            {
                return Result<LandObj>.Failure($"An error occurred while retrieving land details: {ex.Message}", 500);
            }

            return Result<LandObj>.Success(landObj, null, 200);
        }

        public async Task<Result<LandOwnersObj>> GetLandOwnersDetailsAsync(int unitId)
        {
            var lndOwners = new LandOwnersObj();

            try
            {
                var (singleLandOwners, multipleLandOwners, allLandOwners) = await landRepository.GetLandOwnersDetailsAsync(unitId);

                if (!singleLandOwners.Any() && !allLandOwners.Any())
                {
                    return Result<LandOwnersObj>.Success(null, "No land owners in this unit", 204);
                }

                // Summarize areas for own lands
                var forestSum = singleLandOwners.Sum(x => x.AreaInForest);
                var mountainSum = singleLandOwners.Sum(x => x.AreaInMountain);
                var agricultureSum = singleLandOwners.Sum(x => x.AreaInAgriculture);

                // Include shared lands areas
                if (multipleLandOwners.Any())
                {
                    var currentLandId = 0;
                    foreach (var item in multipleLandOwners)
                    {
                        if (currentLandId != item.LandId)
                        {
                            forestSum += item.AreaInForest;
                            mountainSum += item.AreaInMountain;
                            agricultureSum += item.AreaInAgriculture;

                            currentLandId = item.LandId;
                        }
                    }
                }

                var totalSum = forestSum + mountainSum + agricultureSum;

                // Grouping for shared lands
                var baseLand = (from l in multipleLandOwners
                                group l by new
                                {
                                    l.LandId,
                                    l.Municipality,
                                    l.MainNo,
                                    l.SubNo
                                } into grp
                                select new
                                {
                                    grp.Key.LandId,
                                    grp.Key.Municipality,
                                    grp.Key.MainNo,
                                    grp.Key.SubNo
                                });

                var groupList = (from l in baseLand
                                 group l by new
                                 {
                                     l.Municipality,
                                     l.MainNo,
                                     l.SubNo
                                 } into grp
                                 select new
                                 {
                                     grp.Key.Municipality,
                                     grp.Key.MainNo,
                                     grp.Key.SubNo,
                                     NoOfOccurrences = grp.Count()
                                 });

                // Get land unit details
                var landUnits = (from l in multipleLandOwners
                                 group l by new
                                 {
                                     l.LandId,
                                     l.Unit,
                                     l.ParentUnit
                                 } into grp
                                 select new
                                 {
                                     grp.Key.LandId,
                                     Unit = $"[{grp.Key.ParentUnit}] {grp.Key.Unit}"
                                 }).ToList();

                // Process shared land owner list
                var sharedLandOwnerList = (from t in multipleLandOwners
                                           group t by new
                                           {
                                               t.LandId,
                                               t.AreaInForest,
                                               t.AreaInMountain,
                                               t.AreaInAgriculture,
                                               t.Municipality,
                                               t.MainNo,
                                               t.SubNo,
                                               t.Notes
                                           } into grp
                                           join gl in groupList
                                           on new { grp.Key.Municipality, grp.Key.MainNo, grp.Key.SubNo } equals new { gl.Municipality, gl.MainNo, gl.SubNo }
                                           select new OwnerInfo
                                           {
                                               IsSharedLand = true,
                                               LandId = grp.Key.LandId,
                                               LandIdListStr = grp.Key.LandId.ToString(),
                                               Owners = (from lo in multipleLandOwners
                                                         where lo.LandId == grp.Key.LandId
                                                         group lo by new
                                                         {
                                                             lo.SystemUserId,
                                                             lo.DisplayName,
                                                             lo.FullName,
                                                             lo.Email,
                                                             lo.ContactNumber,
                                                             lo.LandOwnerId,
                                                             lo.ContactOwnerLandId,
                                                             lo.AddressLine1,
                                                             lo.AddressLine2,
                                                             lo.AddressCity,
                                                             lo.BankAccountNo,
                                                             lo.Notes
                                                         } into g
                                                         select new OwnerInfo
                                                         {
                                                             LandId = grp.Key.LandId,
                                                             SystemUserId = g.Key.SystemUserId,
                                                             DisplayName = g.Key.DisplayName,
                                                             FullName = g.Key.FullName,
                                                             Email = g.Key.Email,
                                                             ContactNumber = g.Key.ContactNumber,
                                                             LandOwnerId = g.Key.LandOwnerId,
                                                             ContactOwnerLandId = g.Key.ContactOwnerLandId,
                                                             AddLine1 = g.Key.AddressLine1,
                                                             AddLine2 = g.Key.AddressLine2,
                                                             AddCity = g.Key.AddressCity,
                                                             BankAccountNo = g.Key.BankAccountNo,
                                                             Notes = g.Key.Notes
                                                         }).ToList(),
                                               Unit = string.Join("\n", landUnits.Where(x => x.LandId == grp.Key.LandId).Select(x => x.Unit)),
                                               NoOfOccurrences = gl.NoOfOccurrences,
                                               ForestArea = grp.Key.AreaInForest,
                                               MountainArea = grp.Key.AreaInMountain,
                                               AgricultureArea = grp.Key.AreaInAgriculture,
                                               LandOwnerForestShare = Math.Round(grp.Key.AreaInForest > 0 ? Convert.ToDouble(grp.Key.AreaInForest / forestSum * 100) : 0.0, 3, MidpointRounding.AwayFromZero),
                                               LandOwnerAgricultureShare = Math.Round(grp.Key.AreaInAgriculture > 0 ? Convert.ToDouble(grp.Key.AreaInAgriculture / agricultureSum * 100) : 0.0, 3, MidpointRounding.AwayFromZero),
                                               LandOwnerMountainShare = Math.Round(grp.Key.AreaInMountain > 0 ? Convert.ToDouble(grp.Key.AreaInMountain / mountainSum * 100) : 0.0, 3, MidpointRounding.AwayFromZero),
                                               LandOwnerTotalShare = Math.Round(totalSum > 0 ? Convert.ToDouble((grp.Key.AreaInForest + grp.Key.AreaInMountain + grp.Key.AreaInAgriculture) / totalSum * 100) : 0.0, 3, MidpointRounding.AwayFromZero),
                                               LandsCount = 1
                                           }).ToList();

                // Process same ownership shared lands
                var allSharedLandOwnerList = (from t in allLandOwners
                                              group t by new { t.LandId } into grp
                                              select new OwnerInfo
                                              {
                                                  LandId = grp.Key.LandId,
                                                  Owners = (from lo in allLandOwners
                                                            where lo.LandId == grp.Key.LandId
                                                            group lo by new
                                                            {
                                                                lo.SystemUserId,
                                                                lo.FullName,
                                                                lo.DisplayName,
                                                                lo.Email,
                                                                lo.ContactNumber,
                                                                lo.LandOwnerId,
                                                                lo.ContactOwnerLandId,
                                                                lo.AddressLine1,
                                                                lo.AddressLine2,
                                                                lo.AddressCity,
                                                                lo.BankAccountNo,
                                                                lo.Notes
                                                            } into g
                                                            select new OwnerInfo
                                                            {
                                                                LandId = grp.Key.LandId,
                                                                SystemUserId = g.Key.SystemUserId,
                                                                FullName = g.Key.FullName,
                                                                DisplayName = g.Key.DisplayName,
                                                                Email = g.Key.Email,
                                                                ContactNumber = g.Key.ContactNumber,
                                                                LandOwnerId = g.Key.LandOwnerId,
                                                                ContactOwnerLandId = g.Key.ContactOwnerLandId,
                                                                AddLine1 = g.Key.AddressLine1,
                                                                AddLine2 = g.Key.AddressLine2,
                                                                AddCity = g.Key.AddressCity,
                                                                BankAccountNo = g.Key.BankAccountNo,
                                                                Notes = g.Key.Notes
                                                            }).ToList()
                                              }).ToList();

                var landOwners = new List<OwnerInfo>();
                var sameOwnershipSharedLandOwners = new List<OwnerInfo>();
                var sameOwnershipSharedLandIdList = new List<int>();

                foreach (var item in allSharedLandOwnerList)
                {
                    foreach (var owner in item.Owners)
                    {
                        landOwners.Add(new OwnerInfo
                        {
                            LandId = owner.LandId,
                            SystemUserId = owner.SystemUserId
                        });
                    }
                }

                foreach (var item in landOwners.Where(x => !sameOwnershipSharedLandIdList.Contains(x.LandId)))
                {
                    var equalLandIdList = new List<int>();
                    var forestArea = 0.0f;
                    var agricultureArea = 0.0f;
                    var mountainArea = 0.0f;
                    var landCount = 0;

                    var count = landOwners.Count(i => i.LandId == item.LandId);
                    if (count > 1)
                    {
                        var landIdList = landOwners.GroupBy(x => x.LandId)
                            .Where(x => x.Count() == count && x.Key != item.LandId)
                            .Select(x => x.Key).ToList();

                        if (landIdList.Any())
                        {
                            foreach (var landId in landIdList.Except(sameOwnershipSharedLandIdList))
                            {
                                var comparer = new OwnerComparer();
                                var listOne = landOwners.Where(x => x.LandId == item.LandId).ToList();
                                var listTwo = landOwners.Where(x => x.LandId == landId).ToList();
                                var result = listOne.Intersect(listTwo, comparer).ToList();

                                if (result.Count == count)
                                {
                                    if (!equalLandIdList.Contains(item.LandId))
                                        equalLandIdList.Add(item.LandId);

                                    equalLandIdList.Add(landId);

                                    if (!sameOwnershipSharedLandIdList.Contains(item.LandId))
                                        sameOwnershipSharedLandIdList.Add(item.LandId);

                                    if (!sameOwnershipSharedLandIdList.Contains(landId))
                                        sameOwnershipSharedLandIdList.Add(landId);
                                }
                            }
                        }

                        if (equalLandIdList.Count > 0)
                        {
                            var defaultContactOwner = new OwnerInfo();

                            foreach (var landId in equalLandIdList)
                            {
                                var currentOwner = sharedLandOwnerList.FirstOrDefault(x => x.LandId == landId);
                                if (currentOwner != null)
                                {
                                    forestArea += currentOwner.ForestArea;
                                    agricultureArea += currentOwner.AgricultureArea;
                                    mountainArea += currentOwner.MountainArea;
                                    landCount++;
                                }
                            }

                            var equalSharedOwnersList = allSharedLandOwnerList
                                .Where(x => equalLandIdList.Contains(x.LandId))
                                .ToList();

                            var contactDefinedOwners = equalSharedOwnersList
                                .SelectMany(sharedLand => sharedLand.Owners)
                                .Where(owner => owner.ContactOwnerLandId != -1)
                                .Select(owner => new OwnerInfo
                                {
                                    LandId = owner.LandId,
                                    SystemUserId = owner.SystemUserId,
                                    FullName = owner.FullName,
                                    Email = owner.Email,
                                    ContactNumber = owner.ContactNumber,
                                    Notes = owner.Notes
                                })
                                .ToList();

                            if (contactDefinedOwners.Any())
                            {
                                defaultContactOwner = contactDefinedOwners.First();
                            }
                            else
                            {
                                defaultContactOwner = equalSharedOwnersList
                                    .OrderBy(x => x.LandId)
                                    .FirstOrDefault()?
                                    .Owners
                                    .OrderBy(x => x.LandOwnerId)
                                    .FirstOrDefault();
                            }

                            var otherSharedOwners = equalSharedOwnersList
                                .FirstOrDefault(x => x.LandId == defaultContactOwner?.LandId)?
                                .Owners
                                .Where(owner => owner.SystemUserId != defaultContactOwner.SystemUserId)
                                .Select(owner => new OwnerInfo
                                {
                                    LandId = owner.LandId,
                                    SystemUserId = owner.SystemUserId,
                                    FullName = owner.FullName,
                                    Email = owner.Email,
                                    ContactNumber = owner.ContactNumber,
                                    Notes = owner.Notes
                                })
                                .ToList() ?? new List<OwnerInfo>();

                            var ownershipSharedLandOwners = new List<OwnerInfo> { defaultContactOwner };
                            ownershipSharedLandOwners.AddRange(otherSharedOwners);

                            if (sharedLandOwnerList.Any(x => equalLandIdList.Contains(x.LandId)))
                            {
                                var owner = new OwnerInfo
                                {
                                    IsSharedLand = true,
                                    LandId = defaultContactOwner?.LandId ?? 0,
                                    LandIdListStr = string.Join(";", equalLandIdList),
                                    ForestArea = forestArea,
                                    AgricultureArea = agricultureArea,
                                    MountainArea = mountainArea,
                                    LandOwnerForestShare = Math.Round(forestSum > 0 ? forestArea / forestSum * 100 : 0.0, 3),
                                    LandOwnerAgricultureShare = Math.Round(agricultureSum > 0 ? agricultureArea / agricultureSum * 100 : 0.0, 3),
                                    LandOwnerMountainShare = Math.Round(mountainSum > 0 ? mountainArea / mountainSum * 100 : 0.0, 3),
                                    LandOwnerTotalShare = Math.Round(totalSum > 0 ? (forestArea + agricultureArea + mountainArea) / totalSum * 100 : 0.0, 3),
                                    LandsCount = landCount,
                                    Owners = ownershipSharedLandOwners
                                };
                                sameOwnershipSharedLandOwners.Add(owner);
                            }
                        }
                    }
                }

                // Final processing
                var sharedLandOwners = sharedLandOwnerList
                    .Where(x => !sameOwnershipSharedLandIdList.Any(y => y == x.LandId))
                    .ToList();

                var owners = (from t in singleLandOwners
                              group t by new
                              {
                                  t.SystemUserId,
                                  t.FullName,
                                  t.DisplayName,
                                  t.Email,
                                  t.ContactNumber,
                                  t.AddressLine1,
                                  t.AddressLine2,
                                  t.AddressCity,
                                  t.BankAccountNo,
                                  t.Notes
                              } into grp
                              select new OwnerInfo
                              {
                                  IsSharedLand = false,
                                  SystemUserId = grp.Key.SystemUserId,
                                  FullName = grp.Key.FullName,
                                  DisplayName = grp.Key.DisplayName,
                                  Email = grp.Key.Email,
                                  ContactNumber = grp.Key.ContactNumber,
                                  AddLine1 = grp.Key.AddressLine1,
                                  AddLine2 = grp.Key.AddressLine2,
                                  AddCity = grp.Key.AddressCity,
                                  BankAccountNo = grp.Key.BankAccountNo,
                                  Notes = grp.Key.Notes,
                                  ForestArea = grp.Sum(x => x.AreaInForest),
                                  AgricultureArea = grp.Sum(x => x.AreaInAgriculture),
                                  MountainArea = grp.Sum(x => x.AreaInMountain),
                                  LandOwnerForestShare = Math.Round(forestSum > 0 ? Convert.ToDouble((grp.Sum(x => x.AreaInForest) / forestSum) * 100) : 0.0, 3, MidpointRounding.AwayFromZero),
                                  LandOwnerAgricultureShare = Math.Round(agricultureSum > 0 ? Convert.ToDouble((grp.Sum(x => x.AreaInAgriculture) / agricultureSum) * 100) : 0.0, 3, MidpointRounding.AwayFromZero),
                                  LandOwnerMountainShare = Math.Round(mountainSum > 0 ? Convert.ToDouble((grp.Sum(x => x.AreaInMountain) / mountainSum) * 100) : 0.0, 3, MidpointRounding.AwayFromZero),
                                  LandOwnerTotalShare = Math.Round(totalSum > 0 ? Convert.ToDouble(((grp.Sum(x => x.AreaInForest) + grp.Sum(x => x.AreaInAgriculture) + grp.Sum(x => x.AreaInMountain)) / totalSum) * 100) : 0.0, 3, MidpointRounding.AwayFromZero),
                                  LandsCount = grp.Count(),
                              }).ToList();

                owners.AddRange(sharedLandOwners);
                owners.AddRange(sameOwnershipSharedLandOwners);

                lndOwners.LandOwner = owners;
                lndOwners.ForestArea = owners.Sum(x => x.ForestArea);
                lndOwners.AgricultureArea = owners.Sum(x => x.AgricultureArea);
                lndOwners.MountainArea = owners.Sum(x => x.MountainArea);
                lndOwners.LandsCount = owners.Sum(x => x.LandsCount);
                lndOwners.UnitTotalArea = lndOwners.ForestArea + lndOwners.MountainArea + lndOwners.AgricultureArea;
                lndOwners.OwnersCount = owners.Count();

                return Result<LandOwnersObj>.Success(lndOwners, null, 200);
            }
            catch (Exception ex)
            {
                return Result<LandOwnersObj>.Failure($"An error occurred while retrieving land owner details: {ex.Message}", 500);
            }
        }


        public class OwnerComparer : IEqualityComparer<OwnerInfo>
        {
            public bool Equals(OwnerInfo x, OwnerInfo y)
            {
                return x.SystemUserId == y.SystemUserId;
            }

            public int GetHashCode(OwnerInfo x)
            {
                return x.SystemUserId.GetHashCode();
            }
        }


        public class LandOwnerComparer : IEqualityComparer<LandOwnerInfo>
        {
            public bool Equals(LandOwnerInfo x, LandOwnerInfo y)
            {
                return x.SystemUserId == y.SystemUserId;
            }

            public int GetHashCode(LandOwnerInfo x)
            {
                return x.SystemUserId.GetHashCode();
            }
        }

        private static void ManageMultipleLandMultipleSameOwnerShip(IList<OwnerInfo> AllSharedLandOwnerList, ref LandObj landObj)
        {
            var landOwners = new List<LandOwnerInfo>();
            var sameOwnershipSharedLandIdList = new List<int>();

            foreach (var item in AllSharedLandOwnerList)
            {
                foreach (var owner in item.Owners)
                {
                    var landOwner = new LandOwnerInfo
                    {
                        LandId = item.LandId,
                        SystemUserId = owner.SystemUserId
                    };

                    landOwners.Add(landOwner);
                }
            }

            foreach (var item in landOwners.Where(x => !sameOwnershipSharedLandIdList.Contains(x.LandId)))
            {
                var equalLandIdList = new List<int>();

                var count = landOwners.Count(i => i.LandId == item.LandId);  // get current land user count
                if (count > 1)
                {
                    var landIdList = landOwners.GroupBy(x => x.LandId)
                                               .Where(x => x.Count() == count && x.Key != item.LandId)
                                               .Select(x => x.Key).ToList(); // get same count exist land id list

                    if (landIdList.Any())
                    {
                        foreach (var landId in landIdList.Except(sameOwnershipSharedLandIdList))   // comparing each land owner list
                        {
                            var comparer = new LandOwnerComparer();
                            var listOne = landOwners.Where(x => x.LandId == item.LandId).AsEnumerable();
                            var listTwo = landOwners.Where(x => x.LandId == landId).ToList();
                            var result = listOne.Intersect(listTwo, comparer).ToList();  // check wherether lands users same

                            if (result.Count() == count) // check wherether lands count of user same
                            {
                                if (!equalLandIdList.Contains(item.LandId))
                                {
                                    equalLandIdList.Add(item.LandId);
                                }

                                equalLandIdList.Add(landId);

                                if (!sameOwnershipSharedLandIdList.Contains(item.LandId))
                                {
                                    sameOwnershipSharedLandIdList.Add(item.LandId);
                                }

                                if (!sameOwnershipSharedLandIdList.Contains(landId))
                                {
                                    sameOwnershipSharedLandIdList.Add(landId);
                                }
                            }
                        }
                    }

                    if (equalLandIdList.Count > 0)
                    {
                        var defautContactOwner = new LandOwnerInfo();
                        var equalSharedOwnersList = AllSharedLandOwnerList.Where(x => equalLandIdList.Any(y => y == x.LandId)).ToList();
                        var contactExistOwners = new List<LandOwnerInfo>();

                        foreach (var sharedOwnedLand in equalSharedOwnersList) // finding out contact defined owner
                        {
                            foreach (var sharedOwner in sharedOwnedLand.Owners.ToList())
                            {
                                if (sharedOwner.ContactOwnerLandId != -1)
                                {
                                    var contactExistOwner = new LandOwnerInfo
                                    {
                                        LandId = sharedOwner.LandId,
                                        SystemUserId = sharedOwner.SystemUserId,
                                        FullName = sharedOwner.FullName,
                                        DisplayName = sharedOwner.DisplayName,
                                        Email = sharedOwner.Email,
                                        ContactNumber = sharedOwner.ContactNumber,
                                        ContactOwnerLandId = sharedOwner.ContactOwnerLandId,
                                        LandOwnerId = sharedOwner.LandOwnerId
                                    };

                                    contactExistOwners.Add(contactExistOwner);
                                }
                            }
                        }

                        if (contactExistOwners.Any()) // if contact defined owner exist for land
                        {
                            defautContactOwner = contactExistOwners.FirstOrDefault();
                        }
                        else
                        {
                            defautContactOwner = equalSharedOwnersList.OrderBy(x => x.LandId)
                                                                       .FirstOrDefault()
                                                                       .Owners.OrderBy(x => x.LandOwnerId)
                                                                       .Select(x => new LandOwnerInfo
                                                                       {
                                                                           LandId = x.LandId,
                                                                           SystemUserId = x.SystemUserId,
                                                                           FullName = x.FullName,
                                                                           DisplayName = x.DisplayName,
                                                                           Email = x.Email,
                                                                           ContactNumber = x.ContactNumber,
                                                                           ContactOwnerLandId = x.ContactOwnerLandId,
                                                                           LandOwnerId = x.LandOwnerId
                                                                       }).FirstOrDefault(); // if no contact defined owner then first registered owner
                                                                                            //  // of first land set as default contact owner
                        }

                        var ownershipSharedLandOwners = new List<LandOwnerInfo>();
                        var otherSharedOwners = equalSharedOwnersList.FirstOrDefault(x => x.LandId == defautContactOwner.LandId)
                                                                       .Owners.Where(x => x.SystemUserId != defautContactOwner.SystemUserId)
                                                                       .Select(x => new LandOwnerInfo
                                                                       {
                                                                           LandId = x.LandId,
                                                                           SystemUserId = x.SystemUserId,
                                                                           FullName = x.FullName,
                                                                           DisplayName = x.DisplayName,
                                                                           Email = x.Email,
                                                                           ContactNumber = x.ContactNumber,
                                                                           ContactOwnerLandId = x.ContactOwnerLandId,
                                                                           LandOwnerId = x.LandOwnerId
                                                                       }).ToList();

                        ownershipSharedLandOwners.Add(defautContactOwner);
                        ownershipSharedLandOwners.AddRange(otherSharedOwners);

                        foreach (var land in equalSharedOwnersList)
                        {
                            var targetLand = landObj.Lands.FirstOrDefault(x => x.LandId == land.LandId);
                            if (targetLand != null)
                            {
                                targetLand.LandOwners = ownershipSharedLandOwners;
                            }
                        }
                    }
                }
            }

            var nonMultipleSameOwnershipLands = landObj.Lands.Where(x => !sameOwnershipSharedLandIdList.Any(y => y == x.LandId)).ToList();

            foreach (var land in nonMultipleSameOwnershipLands)
            {
                if (land.LandOwners.Count > 1)
                {
                    land.LandOwners = land.LandOwners.OrderByDescending(x => x.ContactOwnerLandId)
                                                     .ThenBy(x => x.LandOwnerId)
                                                     .ToList();
                }
            }

        }

        //public async Task<LandObj> ArchiveLandAsync(ArchiveLand archiveLand)
        //{
        //    var landObj = new LandObj();
        //    var id = await landRepository.ArchiveLandAsync(archiveLand.LandId, archiveLand.DeletedBy);
        //    if (id > 0)
        //    {
        //       var landsResult  = await GetLandDetailsAsync(archiveLand.UnitId, false);
        //        if(landsResult.IsSuccess && landsResult.Data != null)
        //        {
        //            landObj = landsResult.Data;
        //        }
        //    }
        //    return landObj;
        //}






        public async Task<Result<LandObj>> ArchiveLandAsync(ArchiveLand archiveLand)
        {
            try
            {
                var id = await landRepository.ArchiveLandAsync(archiveLand.LandId, archiveLand.DeletedBy);

                if (id <= 0)
                {
                    return Result<LandObj>.Failure("Failed to archive the land.", 400);
                }

                var landsResult = await GetLandDetailsAsync(archiveLand.UnitId, false);

                if (!landsResult.IsSuccess)
                {
                    return Result<LandObj>.Failure(landsResult.Message, landsResult.StatusCode);
                }

                return Result<LandObj>.Success(landsResult.Value, landsResult.Message, landsResult.StatusCode);
            }
            catch (Exception ex)
            {
                return Result<LandObj>.Failure($"An error occurred while archiving the land: {ex.Message}", 500);
            }
        }

        public async Task<int> ManageLandOwnerDetails(OwnerDetail ownerDetail)
        {

            return await landRepository.ManageLandOwnerDetails(ownerDetail);

        }

        public async Task<LandOwnerRegisterDetail> GetLandOwnerDetailByLandIdAsync(int landId)
        {
            var landOwnerRegisterDetail = new LandOwnerRegisterDetail();
            //      var landOwners = await systemContext.Lands
            //.Where(l => l.LandId == landId && l.IsActive)
            //.Join(
            //    systemContext.LandOwners.Where(lo => lo.IsActive),
            //    l => l.LandId,
            //    lo => lo.LandId,
            //    (l, lo) => new { Land = l, LandOwner = lo }
            //)
            //.Join(
            //    systemContext.ViltraUsers,
            //    combined => combined.LandOwner.SystemUserId,
            //    vu => vu.UserId,
            //    (combined, vu) => new { combined.Land, combined.LandOwner, ViltraUser = vu }
            //)
            //.GroupJoin(
            //    systemContext.LandOwnerDetails
            //        .Where(od => od.LandId == landId && !od.IsArchived),
            //    combined => combined.LandOwner.SystemUserId,
            //    od => od.SystemUserId,
            //    (combined, od) => new { combined.Land, combined.LandOwner, combined.ViltraUser, LandOwnerDetails = od }
            //)
            //.SelectMany(
            //    combinedWithDetails => combinedWithDetails.LandOwnerDetails.DefaultIfEmpty(),
            //    (combinedWithDetails, od) => new OwnersState()
            //    {
            //        SystemUserId = combinedWithDetails.LandOwner.SystemUserId,
            //        FullName = combinedWithDetails.ViltraUser.FirstName + " " + combinedWithDetails.ViltraUser.LastName,
            //        IsSharedLandOwner = od != null ? true : false
            //    }
            //)
            //.OrderBy(result => result.SystemUserId)
            //.ToListAsync();
            var landOwners = await landRepository.GetLandOwnerDetailByLandIdAsync(landId);
            if (landOwners != null)
            {
                var sharedOwner = landOwners.FirstOrDefault(x => x.IsSharedLandOwner);
                if (sharedOwner != null)
                {
                    var ownerDetail = await GetLandOwnerDetailsBySysUIdAsync(sharedOwner.SystemUserId, landId);
                    landOwnerRegisterDetail = new LandOwnerRegisterDetail()
                    {
                        OwnersStates = landOwners,
                        LandId = ownerDetail.LandId,
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
                    var landOwner = landOwners.FirstOrDefault();
                    var ownerDetail = await GetLandOwnerDetailsBySysUIdAsync(landOwner.SystemUserId, landId);
                    landOwnerRegisterDetail = new LandOwnerRegisterDetail()
                    {
                        LandId = ownerDetail.LandId,
                        OwnersStates = landOwners,
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
            }
            return landOwnerRegisterDetail;
        }

        public async Task<Result<MultipleLandOwner>> ManageLandDetails(LandDetail model)
        {
            var multipleLandOwner = new MultipleLandOwner();

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    int landId = -1;

                    if (model.Id == 0)
                    {
                        var land = new ViltrapportenApi.Data.SystemModels.Land
                        {
                            Municipality = model.Municipality,
                            MainNo = model.MainNo,
                            SubNo = model.SubNo,
                            PlotNo = model.PlotNo,
                            OwnershipTypeId = model.OwnershipTypeId,
                            AreaInForest = model.AreaInForest,
                            AreaInMountain = model.AreaInMountain,
                            AreaInAgriculture = model.AreaInAgriculture,
                            TotalArea = model.TotalArea,
                            Notes = model.Notes,
                            IsActive = true,
                            CreatedBy = model.CreatedBy,
                            CreatedOn = DateTime.Now
                        };

                        await systemContext.Lands.AddAsync(land);
                        await systemContext.SaveChangesAsync();

                        landId = land.LandId;
                    }
                    else
                    {
                        var land = await systemContext.Lands.FindAsync(model.Id);
                        if (land == null)
                        {
                            return Result<MultipleLandOwner>.Failure("Land not found", 404);
                        }

                        land.Municipality = model.Municipality;
                        land.MainNo = model.MainNo;
                        land.SubNo = model.SubNo;
                        land.PlotNo = model.PlotNo;
                        land.OwnershipTypeId = model.OwnershipTypeId;
                        land.AreaInForest = model.AreaInForest;
                        land.AreaInMountain = model.AreaInMountain;
                        land.AreaInAgriculture = model.AreaInAgriculture;
                        land.TotalArea = model.TotalArea;
                        land.IsActive = true;
                        land.LastUpdatedBy = model.CreatedBy;
                        land.LastUpdatedDate = DateTime.Now;
                        land.Notes = model.Notes;

                        await systemContext.SaveChangesAsync();
                        landId = model.Id;

                        // Remove existing land units
                        var landUnits = await systemContext.LandUnits
                            .Where(l => l.LandId == landId)
                            .ToListAsync();

                        if (landUnits.Any())
                        {
                            systemContext.LandUnits.RemoveRange(landUnits);
                            await systemContext.SaveChangesAsync();
                        }
                    }

                    if (landId > 0)
                    {
                        // (1) Add units to land
                        foreach (var unit in model.LandUnits)
                        {
                            var curLandUnit = new ViltrapportenApi.Data.SystemModels.LandUnit
                            {
                                LandId = landId,
                                UnitId = unit.UnitId,
                                LandTypeId = unit.LandTypeId
                            };
                            await systemContext.LandUnits.AddAsync(curLandUnit);
                        }
                        await systemContext.SaveChangesAsync();

                        // (2) Manage landowners
                        foreach (var owner in model.Landowners)
                        {
                            await landRepository.ManageLandOwnerAsync(landId, owner.Id, model.CreatedBy);
                        }

                        // Archive removed landowners on edit mode
                        if (model.Id > 0)
                        {
                            var existingOwnerIds = model.Landowners.Select(o => o.Id).ToList();
                            var ownersToArchive = model.ArchivedLandowners.Where(o => !existingOwnerIds.Contains(o.Id));

                            foreach (var owner in ownersToArchive)
                            {
                                await landRepository.ArchiveLandOwnerAsync(model.Id, owner.Id);
                            }
                        }

                        if (model.TeigId > 0)
                        {
                            var entryToUpdate = await mapContext.Teigs.FirstOrDefaultAsync(x => x.Teigid == model.TeigId && !x.IsArchived);
                            var land = await systemContext.Lands.FirstOrDefaultAsync(x => x.LandId == landId);
                            if (entryToUpdate != null && land != null)
                            {
                                var landLayer = new LandDrawn
                                {
                                    LandDrawnId = 0,
                                    Geometry = entryToUpdate.Omrade,
                                    Properties = null,
                                    UuidLandDrawn = Guid.NewGuid(),
                                    CreatedBy = model.CreatedBy,
                                    EditedBy = null,
                                    LandId = landId,
                                    TeigId = model.TeigId,
                                    MunicipalityNo = entryToUpdate.Kommunenummer,
                                    MunicipalityName = entryToUpdate.Kommunenavn,
                                    MainNo = land.MainNo,
                                    SubNo = land.SubNo,
                                    PlotNo = land.PlotNo
                                };

                                await mapContext.LandDrawns.AddAsync(landLayer);

                                entryToUpdate.IsModified = true; //***
                                entryToUpdate.EditedBy = model.CreatedBy;

                                mapContext.Entry(entryToUpdate).State = EntityState.Modified;
                            }
                            else { return Result<MultipleLandOwner>.Failure("No valid land or layer fould.", 404); }
                        }
                        // Determine if multiple owners with same ownership exist
                        if (model.Landowners.Count > 1)
                        {
                            var allMultipleOwnershipLand = await landRepository.GetLandMultipleOwnersContactOwnerDetailsAsync();
                            GetMultipleLandMultipleSameOwnerShipStatus(landId, allMultipleOwnershipLand, ref multipleLandOwner);
                        }
                        else
                        {
                            multipleLandOwner = new MultipleLandOwner()
                            {
                                TeigId = model.TeigId,
                                IsSameMultipleLandExist = false,
                                ContactPersonName = string.Empty,
                                IsSameLand = false,
                                IsSuccess = true
                            };
                        }
                    }

                    await systemContext.SaveChangesAsync();
                    await mapContext.SaveChangesAsync();

                    transactionScope.Complete();

                    return Result<MultipleLandOwner>.Success(multipleLandOwner, "Land details managed successfully.");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<LandOwnerMapped> GetLandOwnerDetailsBySysUIdAsync(int systemUserId, int landId)
        {
            return await landRepository.GetLandOwnerDetailsBySysUIdAsync(systemUserId, landId);
        }

        private static void GetMultipleLandMultipleSameOwnerShipStatus(int curLandId, List<LandOwnerMapped> allMultipleOwnershipLand, ref MultipleLandOwner multipleLandOwner)
        {
            var landOwners = new List<OwnerInfo>();
            var sameOwnershipSharedLandIdList = new List<int>();

            var allSharedLandOwnerList = (from t in allMultipleOwnershipLand
                                          group t by new { t.LandId } into grp
                                          select new OwnerInfo
                                          {
                                              LandId = grp.Key.LandId,
                                              Owners = (from lo in allMultipleOwnershipLand
                                                        where lo.LandId == grp.Key.LandId
                                                        group lo by new
                                                        {
                                                            lo.SystemUserId,
                                                            lo.FullName,
                                                            lo.DisplayName,
                                                            lo.Email,
                                                            lo.ContactNumber,
                                                            lo.LandOwnerId,
                                                            lo.ContactOwnerLandId,
                                                            lo.AddressLine1,
                                                            lo.AddressLine2,
                                                            lo.AddressCity,
                                                            lo.BankAccountNo,
                                                            lo.Notes
                                                        } into g
                                                        select new OwnerInfo
                                                        {
                                                            LandId = grp.Key.LandId,
                                                            SystemUserId = g.Key.SystemUserId,
                                                            FullName = g.Key.FullName,
                                                            DisplayName = g.Key.DisplayName,
                                                            Email = g.Key.Email,
                                                            ContactNumber = g.Key.ContactNumber,
                                                            LandOwnerId = g.Key.LandOwnerId,
                                                            ContactOwnerLandId = g.Key.ContactOwnerLandId,
                                                            AddLine1 = g.Key.AddressLine1,
                                                            AddLine2 = g.Key.AddressLine2,
                                                            AddCity = g.Key.AddressCity,
                                                            BankAccountNo = g.Key.BankAccountNo,
                                                            Notes = g.Key.Notes
                                                        }).ToList()
                                          }).ToList();

            foreach (var item in allSharedLandOwnerList)
            {
                foreach (var owner in item.Owners)
                {
                    landOwners.Add(new OwnerInfo
                    {
                        LandId = owner.LandId,
                        SystemUserId = owner.SystemUserId
                    });
                }
            }

            foreach (var item in landOwners.Where(x => !sameOwnershipSharedLandIdList.Contains(x.LandId)))
            {
                var equalLandIdList = new List<int>(); // equal owners exist land list
                var count = landOwners.Count(i => i.LandId == item.LandId);
                if (count > 1)
                {
                    var landIdList = landOwners.GroupBy(x => x.LandId)
                        .Where(x => x.Count() == count && x.Key != item.LandId)
                        .Select(x => x.Key).ToList();

                    if (landIdList.Any())
                    {
                        foreach (var landId in landIdList.Except(sameOwnershipSharedLandIdList))
                        {
                            var comparer = new OwnerComparer();
                            var listOne = landOwners.Where(x => x.LandId == item.LandId).ToList();
                            var listTwo = landOwners.Where(x => x.LandId == landId).ToList();
                            var result = listOne.Intersect(listTwo, comparer).ToList();

                            if (result.Count == count)
                            {
                                if (!equalLandIdList.Contains(item.LandId))
                                    equalLandIdList.Add(item.LandId);

                                equalLandIdList.Add(landId);

                                if (!sameOwnershipSharedLandIdList.Contains(item.LandId))
                                    sameOwnershipSharedLandIdList.Add(item.LandId);

                                if (!sameOwnershipSharedLandIdList.Contains(landId))
                                    sameOwnershipSharedLandIdList.Add(landId);
                            }
                        }
                    }

                    if (equalLandIdList.Count > 0)
                    {
                        var isSameMultipleLandExist = equalLandIdList.Any(x => x == curLandId);
                        if (isSameMultipleLandExist)
                        {
                            var defautContactOwner = new OwnerInfo();
                            var equalSharedOwnersList = allSharedLandOwnerList.Where(x => equalLandIdList.Any(y => y == x.LandId)).ToList(); // get the equal owners land list from unit land lists
                            var contactExistOwners = new List<OwnerInfo>();

                            foreach (var sharedOwnedLand in equalSharedOwnersList) // finding out contact defined owner
                            {
                                foreach (var sharedOwner in sharedOwnedLand.Owners.ToList())
                                {
                                    if (sharedOwner.ContactOwnerLandId != -1)
                                    {
                                        var contactExistOwner = new OwnerInfo()
                                        {
                                            LandId = sharedOwner.LandId,
                                            SystemUserId = sharedOwner.SystemUserId,
                                            FullName = sharedOwner.FullName,
                                            DisplayName = sharedOwner.DisplayName,
                                            Email = sharedOwner.Email,
                                            ContactNumber = sharedOwner.ContactNumber,
                                            ContactOwnerLandId = sharedOwner.ContactOwnerLandId,
                                            LandOwnerId = sharedOwner.LandOwnerId
                                        };
                                        contactExistOwners.Add(contactExistOwner);
                                    }
                                }
                            }

                            if (contactExistOwners.Any()) // if contact defined owner exist for land
                            {
                                defautContactOwner = contactExistOwners.FirstOrDefault();
                            }
                            else
                            {
                                defautContactOwner = equalSharedOwnersList.OrderBy(x => x.LandId).FirstOrDefault().Owners.OrderBy(x => x.LandOwnerId).Select(x => new OwnerInfo()
                                {
                                    LandId = x.LandId,
                                    SystemUserId = x.SystemUserId,
                                    FullName = x.FullName,
                                    DisplayName = x.DisplayName,
                                    Email = x.Email,
                                    ContactNumber = x.ContactNumber,
                                    ContactOwnerLandId = x.ContactOwnerLandId,
                                    LandOwnerId = x.LandOwnerId
                                }).FirstOrDefault(); // if no contact defined owner then first registered owner                                                                                                                                   

                                // of first land set as default contact owner
                            }

                            multipleLandOwner = new MultipleLandOwner()
                            {
                                IsSameMultipleLandExist = isSameMultipleLandExist,
                                ContactPersonName = defautContactOwner.FullName,
                                IsSameLand = (defautContactOwner.LandId == curLandId),
                                IsSuccess = true
                            };
                            break;
                        }
                    }
                }
            }

            if (!multipleLandOwner.IsSuccess)
            {
                multipleLandOwner.IsSuccess = true; // the land is multiple owners with no any matching same users land
            }
        }

    }

}


//var result = await _context.Lands.Where(x => x.LandId == landId && x.IsActive).
//    Join(_context.LandOwners.Where(x => x.IsActive && x.LandId == landId && x.IsActive).
//    Join(_context.ViltraUsers, o => o.SystemUserId, v => v.UserId, (o, v) => new { owner = o, user = v }).
//    Select(x => new { landId = x.owner.LandId, systemUserId = x.owner.SystemUserId, firstName = x.user.FirstName, lastName = x.user.LastName, contactNo = x.user.ContactNumber, email = x.user.Email }).
//    GroupBy(x => new { x.landId, x.systemUserId, x.firstName, x.lastName, x.contactNo, x.email }), l => l.LandId, o => o.Key.landId, (l, o) => new { land = l, owner = o }).
//    Select(x => new LandInformation()
//    {
//        LandId = x.land.LandId,
//        LandOwners = (IList<LandOwnerInformation>)x.owner.Select(x => new LandOwnerInformation() { LandId = x.landId, SystemUserId = x.systemUserId, OwnerName = $"{x.firstName}  {x.lastName}" }).AsEnumerable(),
//    }).FirstOrDefaultAsync();

//return  result;