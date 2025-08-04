using GeoJSON.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;
using ViltrapportenApi.Data.CustomModels;
using ViltrapportenApi.Data.MapModels;
using ViltrapportenApi.Data.SystemModels;
using ViltrapportenApi.Errors;
using ViltrapportenApi.Modal;
using ViltrapportenApi.Services;
using static ViltrapportenApi.Modal.ApiRoutes;

namespace ViltrapportenApi.Controllers
{
    [Authorize]
    public class MapController(IMapDataService mapDataService, ILandService landService, ILandTeigService landTeigService, IStringLocalizer<AuthController> localizer) : BaseApiController
    {
        [HttpGet(ApiRoutes.Map.Maps)]
        public async Task<IActionResult> GetMap()
        {
            GeoJsonConverter convertor = new GeoJsonConverter();
            var result = await mapDataService.GetMapServiceAsync();
            string geoJson = convertor.ConvertEiendomsgrenseToGeoJson(result);

            return Ok(geoJson);
        }

        [HttpPost(ApiRoutes.Map.SelectedMap)]
        public async Task<ActionResult<ApiResponse<MapLandInformation>>> GetSelectedMap([FromBody] LandDto land)
        {
            var landList = new List<LandTeigDTO>();
            PolygonGeoJsonConvertor converter = new PolygonGeoJsonConvertor();

            if (land.LandId > 0)
            {
                var landDrawnResult = await mapDataService.GetSelectedLandAsync(land.LandId);

                if (landDrawnResult.IsSuccess && landDrawnResult.Value != null && landDrawnResult.Value.Any())
                {
                    foreach (var item in landDrawnResult.Value)
                    {
                        var landTeigDTO = landTeigService.GetLandTeigDataFromLandTable(item);

                        if (landTeigDTO != null)
                        {
                            landList.Add(landTeigDTO);
                        }
                    }
                }
            }
            else
            {
                var landDrawnResult = await mapDataService.GetSelectedLandAsync(land.Municipality, land.MainNo, land.SubNo, land.PlotNo);
                if (landDrawnResult.IsSuccess)
                {
                    foreach (var item in landDrawnResult.Value)
                    {
                        var landTeigDTO = landTeigService.GetLandTeigDataFromLandTable(item);

                        if (landTeigDTO != null)
                        {
                            landList.Add(landTeigDTO);
                        }
                    }
                }
            }

            var teigListResult = await mapDataService.GetSelectedMapServiceAsync(land.Municipality, land.MainNo, land.SubNo, land.PlotNo);
            if (teigListResult.IsSuccess)
            {
                foreach (var item in teigListResult.Value)
                {
                    var teig = landTeigService.GetLandTeigDataFromTeigTable(item);
                    if (teig != null)
                    {
                        landList.Add(teig);
                    }
                }
            }

            if (landList == null || !landList.Any())
            {
                return Ok(new ApiResponse<MapLandInformation>(StatusCodes.Status204NoContent, null, localizer["NoLandContent"]));
            }

            string geoJson = null;
            List<LandInformation> landInformations = new List<LandInformation>();

            if (land.LandId != 0)
            {
                var landInfoResult = await landService.GetSelectedLandServiceAsync(land.LandId);
                if (landInfoResult.IsSuccess)
                {
                    landInformations = new List<LandInformation> { landInfoResult.Value };
                }

                geoJson = converter.ConvertTeigToGeoJson(landList, landInfoResult.Value.LandUnits, land.LandId, land.MainNo, land.SubNo, land.PlotNo);
                if (string.IsNullOrEmpty(geoJson))
                {
                    return Ok(new ApiResponse<MapLandInformation>(StatusCodes.Status204NoContent, null, localizer["NoLandContent"]));
                }
            }
            else
            {
                var landInfoResult = await landService.GetSelectedLandsServiceAsync(land.Municipality, land.MainNo, land.SubNo, land.PlotNo);
                if (landInfoResult.IsSuccess)
                {
                    landInformations = landInfoResult.Value;
                    if (landInfoResult.Value != null && landInformations.Count == 1)
                    {
                        geoJson = converter.ConvertTeigToGeoJson(landList, landInfoResult.Value.FirstOrDefault().LandUnits, landInformations.First().LandId, land.MainNo, land.SubNo, land.PlotNo);
                    }
                    else
                    {
                        geoJson = converter.ConvertTeigToGeoJson(landList, null, null, null, null, null);
                    }
                    if (string.IsNullOrEmpty(geoJson))
                    {
                        return Ok(new ApiResponse<MapLandInformation>(StatusCodes.Status204NoContent, null, localizer["NoLandContent"]));
                    }
                }
            }

            var mapLandInfo = new MapLandInformation
            {
                MapGeoJson = geoJson,
                LandInformations = landInformations
            };

            return Ok(new ApiResponse<MapLandInformation>(StatusCodes.Status200OK, mapLandInfo, "Succesfully retrive data"));
        }

        [HttpGet(ApiRoutes.Map.UnitLandLayers)]
        public async Task<ActionResult<ApiResponse<LandObj>>> GetUnitLandLayers(int unitId)
        {
            var landsInfoResult = await landService.GetLandDetailsAsync(unitId, true);

            if (!landsInfoResult.IsSuccess)
            {
                return StatusCode(landsInfoResult.StatusCode, new ApiResponse<LandObj>(landsInfoResult.StatusCode, null, landsInfoResult.Message));
            }

            var landsInfo = landsInfoResult.Value;
            if (landsInfo == null || landsInfo.Lands == null || !landsInfo.Lands.Any())
            {
                return Ok(new ApiResponse<LandObj>(StatusCodes.Status204NoContent, null, localizer["NoLandContent"]));
            }

            PolygonGeoJsonConvertor converter = new PolygonGeoJsonConvertor();

              var i = 0;
            foreach (var land in landsInfo.Lands)
            {
                i++;
              // if (i is  2)
                //  continue;
                List<LandTeigDTO> landList = new List<LandTeigDTO>();
                if (land == null)
                {
                    continue;
                }

                int.TryParse(land.MainNo, out int mainNo);
                int.TryParse(land.SubNo, out int subNo);
                int? plotNo = null;

                if (!string.IsNullOrEmpty(land.PlotNo) && int.TryParse(land.PlotNo, out int parsedPlotNo))
                {
                    plotNo = parsedPlotNo;
                }

                // Initialize landDrawnResult with a default failure result
                Result<IList<LandDrawn>> landDrawnResult = Result<IList<LandDrawn>>.Failure("No land drawn data found.", 404);
                if (land.LandId > 0)
                {
                    landDrawnResult = await mapDataService.GetSelectedLandAsync(land.LandId);
                }

                string geoJson = null;

                if (landDrawnResult.IsSuccess && landDrawnResult.Value != null && landDrawnResult.Value.Any())
                {
                    foreach (var item in landDrawnResult.Value)
                    {

                        var landDrawn = landTeigService.GetLandTeigDataFromLandTable(item);

                        if (landDrawn != null)
                        {
                            landList.Add(landDrawn);
                        }
                    }
                    // Convert land drawn to GeoJSON
                    //  geoJson = converter.ConvertLandToGeoJson(landDrawnResult.Value, land.LandUnits, land.LandId, mainNo, subNo, plotNo);
                }

                //else
                //{
                var mapDataResult = await mapDataService.GetSelectedMapServiceAsync(land.Municipality, mainNo, subNo, plotNo);

                if (mapDataResult.IsSuccess && mapDataResult.Value != null && mapDataResult.Value.Any())
                {
                    foreach (var item in mapDataResult.Value)
                    {
                        var teig = landTeigService.GetLandTeigDataFromTeigTable(item);

                        if (teig != null)
                        {
                            landList.Add(teig);
                        }
                    }

                }
                geoJson = converter.ConvertTeigToGeoJson(landList, land.LandUnits, land.LandId, mainNo, subNo, plotNo);

                //}
                // Assign GeoJSON to land if available
                if (!string.IsNullOrEmpty(geoJson))
                {
                    land.MapGeoJson = geoJson;
                }
               //if (i ==1)
               //s   break;
            }
            return Ok(new ApiResponse<LandObj>(StatusCodes.Status200OK, landsInfo, "successfully got land data... "));
        }

        [HttpGet(ApiRoutes.Map.UnitOwnersLandLayers)]
        public async Task<ActionResult<ApiResponse<LandObj>>> GetUnitOwnersLandLayers(int unitId, int ownerUId, bool isDnnId, bool isLandTab)
        {/***/
            var ownersLandInfoResult = await landService.GetOwnersLandDetailsUnderUnitAsync(ownerUId, isDnnId, unitId, isLandTab);
            if (!ownersLandInfoResult.IsSuccess)
            {
                return Ok(new ApiResponse<LandObj>(ownersLandInfoResult.StatusCode, null, ownersLandInfoResult.Message));
            }
            //if (ownersLandInfoResult == null)
            //{
            //    return Ok(new ApiResponse(StatusCodes.Status204NoContent, localizer["NoLandContent"]));
            //}

            var landList = new List<LandTeigDTO>();
            PolygonGeoJsonConvertor converter = new PolygonGeoJsonConvertor();

            foreach (var land in ownersLandInfoResult.Value.Lands)
            {
                if (land == null)
                {
                    continue;
                }

                int.TryParse(land.MainNo, out int mainNo);
                int.TryParse(land.SubNo, out int subNo);
                int? plotNo = null;

                if (!string.IsNullOrEmpty(land.PlotNo) && int.TryParse(land.PlotNo, out int parsedPlotNo))
                {
                    plotNo = parsedPlotNo;
                }

                var landDrawnResult = Result<IList<LandDrawn>>.Failure("No land drawn data found.", 404);
                if (land.LandId > 0)
                {
                    landDrawnResult = await mapDataService.GetSelectedLandAsync(land.LandId);
                }

                string geoJson = null;

                if (landDrawnResult.IsSuccess && landDrawnResult.Value != null && landDrawnResult.Value.Any())
                {
                    foreach (var item in landDrawnResult.Value)
                    {

                        var landDrawn = landTeigService.GetLandTeigDataFromLandTable(item);

                        if (landDrawn != null)
                        {
                            landList.Add(landDrawn);
                        }
                    }
                    // Convert land drawn to GeoJSON
                    // geoJson = converter.ConvertLandToGeoJson(landDrawnResult.Value, land.LandUnits, land.LandId, mainNo, subNo, plotNo);
                }

                var mapDataResult = await mapDataService.GetSelectedMapServiceAsync(land.Municipality, mainNo, subNo, plotNo);

                if (mapDataResult.IsSuccess && mapDataResult.Value != null && mapDataResult.Value.Any())
                {
                    foreach (var item in mapDataResult.Value)
                    {
                        var teig = landTeigService.GetLandTeigDataFromTeigTable(item);

                        if (teig != null)
                        {
                            landList.Add(teig);
                        }
                    }
                }
                geoJson = converter.ConvertTeigToGeoJson(landList, land.LandUnits, land.LandId, mainNo, subNo, plotNo);


                // Assign GeoJSON to land if available
                if (!string.IsNullOrEmpty(geoJson))
                {
                    land.MapGeoJson = geoJson;
                }
            }
            return Ok(new ApiResponse<LandObj>(ownersLandInfoResult.StatusCode, ownersLandInfoResult.Value, ownersLandInfoResult.Message));
        }



        [HttpPost(ApiRoutes.Map.SaveDrawnFeatures)]
        public async Task<IActionResult> SaveDrawnFeatures([FromBody] DrawFeatureCollection geoJsonFeatureCollection)
        {
            if (geoJsonFeatureCollection == null)
            {
                return BadRequest();
            }

            var result = await mapDataService.SaveDrawnFeatures(geoJsonFeatureCollection);
            PolygonGeoJsonConvertor convertor = new PolygonGeoJsonConvertor();
            return Ok(result);
        }

        [HttpPost(ApiRoutes.Map.SaveModifiedLandFeatures)]
        public async Task<ActionResult<ApiResponse<bool>>> SaveModifiedLandFeatures([FromBody] DrawFeatureCollection geoJsonFeatureCollection)
        {
            if (geoJsonFeatureCollection == null)
            {
                return BadRequest(new ApiResponse<bool>(StatusCodes.Status400BadRequest, false, "Bad Request"));
            }

            var result = await mapDataService.SaveModifiedLandFeatures(geoJsonFeatureCollection);
            if (result.IsSuccess)
            {
                return Ok(new ApiResponse<bool>(result.StatusCode, true, result.Message));
            }
            else
            {
                return Ok(new ApiResponse<bool>(result.StatusCode, false, result.Message));
            }
        }

        [HttpPost(ApiRoutes.Map.DeleteLandLayer)]
        public async Task<IActionResult> DeleteLandLayer([FromBody] DeleteLandLayerReq deleteLandLayerReq)
        {
            if (deleteLandLayerReq == null)
            {
                return BadRequest(new ApiResponse<bool>(StatusCodes.Status400BadRequest, false, "Bad Request"));
            }

            var result = await mapDataService.DeleteLandLayer(deleteLandLayerReq);
            if (result.IsSuccess)
            {
                return Ok(new ApiResponse<bool>(result.StatusCode, true, result.Message));
            }
            else
            {
                return Ok(new ApiResponse<bool>(result.StatusCode, false, result.Message));
            }
        }

        [HttpGet(ApiRoutes.Map.GetDrawnFeatures)]
        public async Task<IActionResult> GetDrawnFeatures()
        {
            var result = await mapDataService.GetDrawnFeatures();
            return Ok(result);
        }

        //[HttpGet(ApiRoutes.Map.BaseMap)]
        //public async Task<IActionResult> GetBaseMap()
        //{
        //    GeoJsonConverter convertor = new GeoJsonConverter();
        //   // var result = await mapDataService.GetBaseMapServiceAsync();
        //   //string geoJson =  convertor.ConvertHoydekurveToGeoJson(result);
        //   // var geoJson = JsonConvert.SerializeObject(result);delete
        //   // return Ok(geoJson);
        //}

        //[HttpPost(ApiRoutes.Map.SelectedMap)]
        //public async Task<IActionResult> GetSelectedMap([FromBody] LandDto land)
        //{
        //    PolygonGeoJsonConvertor converter = new PolygonGeoJsonConvertor();
        //    if (land == null)
        //    {
        //        return BadRequest("Land information is required.");
        //    }

        //    var mapData = await mapDataService.GetSelectedMapServiceAsync(land.Municipality, land.MainNo, land.SubNo, land.PlotNo);
        //    if (mapData == null || !mapData.Any())
        //    {
        //        return NoContent();
        //    }

        //    string geoJson = converter.ConvertTeigToGeoJson(mapData);
        //    if (string.IsNullOrEmpty(geoJson))
        //    {
        //        return NoContent();
        //    }

        //    var landInformations = new List<LandInformation>();
        //    if (land.LandId != 0)
        //    {

        //        var landInfo = await landService.GetSelectedLandServiceAsync(land.LandId);
        //        if (landInfo == null)
        //        {
        //            return NotFound($"Land information for LandId {land.LandId} not found.");
        //        }
        //        else
        //        {
        //            var mapLandInfo = new MapLandInfomation()
        //            {
        //                MapGeoJson = geoJson,
        //                LandInformations = landInformations.Add(landInfo)
        //            };

        //            return Ok(mapLandInfo);
        //        }
        //    }
        //    else
        //    {
        //        var landsInfo = await landService.GetSelectedLandsServiceAsync(land.Municipality, land.MainNo, land.SubNo, land.PlotNo);
        //        if (landsInfo == null)
        //        {
        //            return NotFound($"Land information for LandId {land.LandId} not found.");
        //        }
        //        else
        //        {
        //            var mapLandInfo = new MapLandInfomation()
        //            {
        //                MapGeoJson = geoJson,
        //                LandInformations = landsInfo
        //            };

        //            return Ok(mapLandInfo);
        //        }
        //    }

        //}



        //[HttpPost(ApiRoutes.Map.SaveDrawnFeatures)]
        //public async Task<IActionResult> SaveDrawnFeatures([FromBody] string geoJson)
        //{

        //    string geoJsonString = geoJson.ToString();
        //    if (geoJsonString == null)
        //    {
        //        return BadRequest("Invalid GeoJSON data.");
        //    }

        //    await mapDataService.SaveGeoJsonToDatabase(geoJsonString);
        //    return Ok("Features saved successfully");
        //}

        //private static GeoJSON.Net.Geometry.MultiPolygon ConvertGeometryToGeoJSON(NetTopologySuite.Geometries.MultiPolygon ntsMultiPolygon)
        //{
        //    var geoJSONMultiPolygon = new GeoJSON.Net.Geometry.MultiPolygon(
        //        ntsMultiPolygon.Geometries.Select(ntsPolygon =>
        //            new Polygon(
        //                new List<LineString>
        //                {
        //    new LineString(
        //        ntsPolygon.Coordinates.Select(ntsCoordinate =>
        //            new Position(ntsCoordinate.Y, ntsCoordinate.X)).ToList()
        //    )
        //                }
        //            )).ToList()
        //    );

        //    return geoJSONMultiPolygon;
        //}

        //private static GeoJSON.Net.Feature.FeatureCollection ConvertToGeoJSON(IEnumerable<Building> buildings)
        //{
        //    var featureCollection = new GeoJSON.Net.Feature.FeatureCollection();

        //    foreach (var building in buildings)
        //    {
        //        var properties = new Dictionary<string, object>
        //{
        //    { "Id", building.Id },
        //    { "fKey", building.fKey },
        //    { "Nitelik", building.Nitelik },
        //    { "Blok", building.Blok }
        //};

        //        var geometry = ConvertGeometryToGeoJSON(building.geom);

        //        var feature = new Feature(geometry, properties);
        //        featureCollection.Features.Add(feature);
        //    }

        //    return featureCollection;
        //}
    }
}
