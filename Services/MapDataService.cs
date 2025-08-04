using Microsoft.EntityFrameworkCore;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using ViltrapportenApi.Data.CustomModels;
using ViltrapportenApi.Data.MapModels;
using NetTopologySuite.Geometries;
using Newtonsoft.Json.Linq;
using static ViltrapportenApi.Modal.ApiRoutes;
using ViltrapportenApi.Modal;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Http.HttpResults;
using ViltrapportenApi.Data.SystemModels;

namespace ViltrapportenApi.Services
{
    public class MapDataService(ViltrapportenMapContext context, ViltrapportenSystemContext systemContext) : IMapDataService
    {
        private readonly ViltrapportenMapContext _context = context;
        private readonly ViltrapportenSystemContext _systemContext = systemContext;

        public async Task<List<Eiendomsgrense>> GetMapServiceAsync()
        {
            return await _context.Eiendomsgrenses.Skip(150000).ToListAsync();
        }

        public async Task<Result<List<Teig>>> GetSelectedMapServiceAsync(string municipilityNo, int mainNo, int subNo, int? plotNo)
        {
            if (string.IsNullOrEmpty(municipilityNo))
            {
                return Result<List<Teig>>.Failure("Municipality number is required.", 400);
            }

            if (mainNo <= 0)
            {
                return Result<List<Teig>>.Failure("Main number must be greater than zero.", 400);
            }

            try
            {
                var query = _context.Teigs
                    .Where(x => x.Kommunenummer == municipilityNo &&
                                x.Matrikkelnummertekst.StartsWith($"{mainNo}/") &&
                                !(x.IsArchived || x.IsModified));//&&  && x.Versjonid > 3

                var teigs = await query.ToListAsync();

                var filteredTeigs = teigs
                    .Where(x => SubNoMatches(x.Matrikkelnummertekst, subNo) &&
                                PlotNoMatches(x.Matrikkelnummertekst, plotNo))
                    .ToList();

                if (filteredTeigs == null || !filteredTeigs.Any())
                {
                    return Result<List<Teig>>.Failure("No matching teigs found.", 404);
                }

                // Group by Matrikkelnummertekst, order by versjonid, and select the highest versjonid for each group
                //var resultTeigs = filteredTeigs
                //    .GroupBy(x => x.Matrikkelnummertekst)
                //    .Select(g => g.OrderByDescending(x => x.Versjonid).First())
                //    .ToList();

                // taking last added item to db
                //var resultTeigs = filteredTeigs
                // .GroupBy(x => x.Matrikkelnummertekst)
                // .Select(g => g.OrderByDescending(x => x.Versjonid).ThenByDescending(x => x.Objid).First())
                // .ToList();
                return Result<List<Teig>>.Success(filteredTeigs);
            }
            catch (Exception ex)
            {
                return Result<List<Teig>>.Failure($"An error occurred while fetching teigs: {ex.Message}", 500);
            }
        }

        public async Task<Result<IList<LandDrawn>>> GetSelectedLandAsync(string municipilityNo, int mainNo, int subNo, int? plotNo)
        {
            var query = _context.LandDrawns
                .Where(x => x.MunicipalityNo == municipilityNo &&
                            x.MainNo == mainNo.ToString() &&
                            x.SubNo == subNo.ToString());

            if (plotNo.HasValue)
            {
                query = query.Where(x => x.PlotNo == plotNo.Value.ToString());
            }

            var result = await query.ToListAsync();

            if (result == null)
            {
                return Result<IList<LandDrawn>>.Failure("No matching land drawn found.", 400);
            }

            return Result<IList<LandDrawn>>.Success(result);
        }

        public async Task<Result<IList<LandDrawn>>> GetSelectedLandAsync(int landId)
        {
            IList<LandDrawn> landDrawn = new List<LandDrawn>();
            if (landId > 0)
            {
                landDrawn = await _context.LandDrawns.Where(x => x.LandId == landId && !x.IsArchived).ToListAsync();
            }

            if (landDrawn == null)
            {
                return Result<IList<LandDrawn>>.Failure("No matching land drawn found.", 400);
            }

            return Result<IList<LandDrawn>>.Success(landDrawn);
        }

        public async Task<List<FeatureDto>> SaveDrawnFeatures(DrawFeatureCollection featureCollection)
        {
            // var featureCollection2 = System.Text.Json.JsonSerializer.Serialize(featureCollection);
            var list = new List<FeatureDrawn>();
            var geoJsonReader = new GeoJsonReader();
            foreach (var feature in featureCollection.Features)
            {
                var geometryJson = feature["geometry"].ToString();
                var geometry = geoJsonReader.Read<Geometry>(geometryJson);
                var properties = feature["properties"];// as Dictionary<string, object>;
                                                       //   string? styleValue = null;
                JObject property = JObject.Parse(properties.ToString());
                Guid id = Guid.Parse(property["id"].ToString());
                string? iconType = geometry.GeometryType == "Point" ? property["iconType"].ToString() : null;

                // Extract the style attributes
                //string fillColor = style["fillColor"].ToString();
                //string strokeColor = style["strokeColor"].ToString();
                //int strokeWidth = (int)style["strokeWidth"];
                //if (properties != null && properties.ContainsKey("style"))
                //{
                //   styleValue = properties["style"] as string;                                                                    
                //}

                var featuredrawn = new FeatureDrawn
                {
                    Geometry = geometry,
                    Properties = JsonConvert.SerializeObject(properties),//feature["properties"].ToString(),
                    UuidFeatureDrawn = id,
                    IconType = iconType
                    // Style = styleJson
                };
                list.Add(featuredrawn);
            }

            await _context.FeatureDrawns.AddRangeAsync(list);
            await _context.SaveChangesAsync();

            var featureEntities = await _context.FeatureDrawns.ToListAsync();
            var geoJsonWriter = new GeoJsonWriter();

            return featureEntities.Select(feature =>
            {
                var geometry = feature.Geometry;
                var geometryJson = geoJsonWriter.Write(geometry);
                var geometryType = geometry.GetType().Name;

                GeometryDto geometryDto;

                switch (geometryType)
                {
                    case "Point":
                        geometryDto = JsonConvert.DeserializeObject<PointDto>(geometryJson);
                        break;
                    case "LineString":
                        geometryDto = JsonConvert.DeserializeObject<LineStringDto>(geometryJson);
                        break;
                    case "Polygon":
                        geometryDto = JsonConvert.DeserializeObject<PolygonDto>(geometryJson);
                        break;
                    default:
                        throw new InvalidOperationException("Unsupported geometry type: " + geometryType);
                }

                return new FeatureDto
                {
                    Geometry = geometryDto,
                    Properties = JsonConvert.DeserializeObject<Dictionary<string, object>>(feature.Properties),
                    UuidFeatureDrawn = feature.UuidFeatureDrawn,
                    IconType = feature.IconType
                    // Style = JsonConvert.DeserializeObject<Dictionary<string, object>>(feature.Style)
                };



                //return featureEntities.Select(feature =>
                //{
                //    // Ensure Geometry is correctly serialized to GeoJSON
                //    var geometry = feature.Geometry;
                //    var geometryJson = geoJsonWriter.Write(geometry);
                //    Console.WriteLine($"Properties: {feature.Properties}");
                //    Console.WriteLine($"Style: {feature.Style}");
                //    return new FeatureDto
                //    {
                //        Type = "Feature",
                //        Geometry = JsonConvert.DeserializeObject<GeometryDto>(geometryJson),
                //        properties = jsonconvert.deserializeobject<dictionary<string, object>>(feature.properties),
                //        style = jsonconvert.deserializeobject<dictionary<string, object>>(feature.style)
                //    };
                //}).ToList();
            }).ToList(); ;
        }

        //public async Task<Result<bool>> SaveLandLayer(int landId, int teigId, int createdBy)
        //{
        //    var entryToUpdate = await _context.Teigs.FirstOrDefaultAsync(x => x.Teigid == teigId && !x.IsArchived);
        //    var land = await _systemContext.Lands.FirstOrDefaultAsync(x => x.LandId == landId);
        //    if (entryToUpdate != null && land != null)
        //    {

        //        var landLayer = new LandDrawn
        //        {
        //            LandDrawnId = 0,
        //            Geometry = entryToUpdate.Omrade,
        //            Properties = null,
        //            UuidLandDrawn = Guid.NewGuid(),
        //            CreatedBy = createdBy,
        //            EditedBy = null,
        //            LandId = landId,
        //            TeigId = teigId,
        //            MunicipalityNo = entryToUpdate.Kommunenummer,
        //            MunicipalityName = entryToUpdate.Kommunenavn,
        //            MainNo = land.MainNo,
        //            SubNo = land.SubNo,
        //            PlotNo = land.PlotNo
        //        };

        //        await _context.LandDrawns.AddAsync(landLayer);

        //        entryToUpdate.IsModified = true; //***
        //        entryToUpdate.EditedBy = createdBy;

        //        _context.Entry(entryToUpdate).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //        return Result<bool>.Success(true, "Successfully saved land features.", 200);
        //    }

        //    return Result<bool>.Failure("No valid land or layer fould.", 404);
        //}


        public async Task<Result<bool>> SaveModifiedLandFeatures(DrawFeatureCollection featureCollection)
        {
            var geoJsonReader = new GeoJsonReader();
            var feature = featureCollection.Features.FirstOrDefault();

            if (feature != null)
            {
                var geometryJson = feature["geometry"]?.ToString();
                var geometry = geoJsonReader.Read<Geometry>(geometryJson);

                var properties = feature["properties"];
                var property = JObject.Parse(properties.ToString());
                Guid.TryParse(property["UuidLandDrawn"]?.ToString(), out Guid uuidLandDrawn);
                int landDrawnId = TryParseInt(property, "LandDrawnId");
                int createdBy = TryParseInt(property, "CreatedBy");
                int editedBy = TryParseInt(property, "EditedBy");
                int landId = TryParseInt(property, "LandId");
                int teigId = TryParseInt(property, "TeigId");

                string municipalityName = property["MunicipalityName"]?.ToString();
                string municipalityNo = property["MunicipalityNo"]?.ToString();
                string mainNo = property["MainNo"]?.ToString();
                string subNo = property["SubNo"]?.ToString();
                string plotNo = property["PlotNo"]?.ToString();

                if (featureCollection?.Features == null || featureCollection.Features.Count == 0)
                    return Result<bool>.Failure("No features provided.", 204);

                var featureDict = featureCollection.Features.FirstOrDefault();
                if (featureDict == null)
                    return Result<bool>.Failure("Feature is null.", 204);

                if (!featureDict.TryGetValue("geometry", out var geometryObj) ||
                    !featureDict.TryGetValue("properties", out var propertiesObj))
                {
                    return Result<bool>.Failure("Required fields are missing.", 204);
                }

                if (string.IsNullOrEmpty(municipalityName) || string.IsNullOrEmpty(municipalityNo) || string.IsNullOrEmpty(mainNo) || string.IsNullOrEmpty(subNo))
                    return Result<bool>.Failure("Required properties are missing.", 204);

                if (string.IsNullOrEmpty(geometryJson))
                    return Result<bool>.Failure("Geometry is missing.", 204);

                var landDrawn = new LandDrawn
                {
                    LandDrawnId = landDrawnId > 0 ? landDrawnId : 0,
                    Geometry = geometry,
                    Properties = JsonConvert.SerializeObject(properties),
                    UuidLandDrawn = uuidLandDrawn,
                    CreatedBy = createdBy,
                    EditedBy = editedBy > 0 ? editedBy : null,
                    LandId = landId > 0 ? landId : 0,
                    TeigId = teigId > 0 ? teigId : 0,
                    MunicipalityNo = municipalityNo,
                    MunicipalityName = municipalityName,
                    MainNo = mainNo,
                    SubNo = subNo,
                    PlotNo = plotNo
                };

                if (landDrawn.LandDrawnId > 0)
                {
                    var entryToUpdate = await _context.LandDrawns.FirstOrDefaultAsync(x => x.LandDrawnId == landDrawn.LandDrawnId);
                    if (entryToUpdate != null)
                    {
                        entryToUpdate.LandDrawnId = landDrawn.LandDrawnId;
                        entryToUpdate.Geometry = landDrawn.Geometry;
                        entryToUpdate.Properties = landDrawn.Properties;
                        entryToUpdate.UuidLandDrawn = landDrawn.UuidLandDrawn;
                        entryToUpdate.EditedBy = landDrawn.EditedBy;
                        entryToUpdate.CreatedBy = landDrawn.CreatedBy;
                        entryToUpdate.LandId = landDrawn.LandId;
                        entryToUpdate.TeigId = landDrawn.TeigId;
                        entryToUpdate.MunicipalityNo = landDrawn.MunicipalityNo;
                        entryToUpdate.MunicipalityName = landDrawn.MunicipalityName;
                        entryToUpdate.MainNo = landDrawn.MainNo;
                        entryToUpdate.SubNo = landDrawn.SubNo;
                    }

                    _context.Entry(entryToUpdate).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    await _context.LandDrawns.AddAsync(landDrawn);
                    var entryToUpdate = await _context.Teigs.FirstOrDefaultAsync(x => x.Teigid == landDrawn.TeigId);
                    if (entryToUpdate != null)
                    {
                        entryToUpdate.IsModified = true;
                        entryToUpdate.EditedBy = createdBy;
                    }
                    _context.Entry(entryToUpdate).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                return Result<bool>.Success(true, "Successfully saved land features.", 200);
            }
            return Result<bool>.Failure("No valid feature found.", 204);
        }


        public async Task<Result> DeleteLandLayer(DeleteLandLayerReq deleteLandLayerReq)
        {
            if (deleteLandLayerReq.LandDrawnId > 0)
            {
                var entryToUpdate = await _context.LandDrawns.FirstOrDefaultAsync(x => x.LandDrawnId == deleteLandLayerReq.LandDrawnId);
                if (entryToUpdate != null)
                {
                    entryToUpdate.IsArchived = true;
                    entryToUpdate.EditedBy = deleteLandLayerReq.UserId;
                }

                _context.Entry(entryToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true, "Sucessfully Deleted the imported land layer", 200);
            }
            else if (deleteLandLayerReq.TeigId > 0)
            {
                var entryToUpdate = await _context.Teigs.FirstOrDefaultAsync(x => x.Teigid == deleteLandLayerReq.TeigId);
                if (entryToUpdate != null)
                {
                    entryToUpdate.IsArchived = true;
                    entryToUpdate.EditedBy = deleteLandLayerReq.UserId;
                }

                _context.Entry(entryToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true, "Sucessfully Deleted the system land layer", 200);
            }
            else
            {
                return Result<bool>.Failure("No given land layer in systems", 204);
            }
        }

        public async Task<List<FeatureDto>> GetDrawnFeatures()
        {
            var featureEntities = await _context.FeatureDrawns.ToListAsync();
            var geoJsonWriter = new GeoJsonWriter();

            return featureEntities.Select(feature =>
            {
                var geometry = feature.Geometry;
                var geometryJson = geoJsonWriter.Write(geometry);
                var geometryType = geometry.GetType().Name;

                GeometryDto geometryDto;

                switch (geometryType)
                {
                    case "Point":
                        geometryDto = JsonConvert.DeserializeObject<PointDto>(geometryJson);
                        break;
                    case "LineString":
                        geometryDto = JsonConvert.DeserializeObject<LineStringDto>(geometryJson);
                        break;
                    case "Polygon":
                        geometryDto = JsonConvert.DeserializeObject<PolygonDto>(geometryJson);
                        break;
                    default:
                        throw new InvalidOperationException("Unsupported geometry type: " + geometryType);
                }

                return new FeatureDto
                {
                    Geometry = geometryDto,
                    Properties = JsonConvert.DeserializeObject<Dictionary<string, object>>(feature.Properties),
                    UuidFeatureDrawn = feature.UuidFeatureDrawn,
                    IconType = feature.IconType
                    // Style = JsonConvert.DeserializeObject<Dictionary<string, object>>(feature.Style)
                };
            }).ToList(); ;
        }

        private static bool SubNoMatches(string matrikkelnummertekst, int subNo)
        {
            var parts = matrikkelnummertekst.Split('/');
            if (parts.Length < 2)
            {
                return false;
            }

            var subPart = parts[1];
            var subRanges = subPart.Split(',');

            foreach (var range in subRanges)
            {
                if (range.Contains('-'))
                {
                    // Handle ranges
                    var rangeParts = range.Split('-');
                    if (rangeParts.Length == 2 && int.TryParse(rangeParts[0], out int start) && int.TryParse(rangeParts[1], out int end))
                    {
                        if (subNo == start || subNo == end)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    // Handle individual numbers
                    if (int.TryParse(range, out int number) && number == subNo)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool PlotNoMatches(string matrikkelnummertekst, int? plotNo)
        {
            if (plotNo is not null)
            {
                var parts = matrikkelnummertekst.Split('/');
                if (parts.Length < 3)
                {
                    return true; // even no plot number for selected map layer show in result
                }

                var subPart = parts[2];
                var subRanges = subPart.Split(',');

                foreach (var range in subRanges)
                {
                    if (int.TryParse(range, out int number) && number == plotNo)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return true;
            }
        }

        private static int TryParseInt(JObject property, string key)
        {
            return int.TryParse(property[key]?.ToString(), out int result) ? result : 0;
        }

        //private bool SubNoMatches(string matrikkelnummertekst, int subNo)
        //{
        //    // Example logic: Extract subNo from matrikkelnummertekst and compare
        //    var parts = matrikkelnummertekst.Split('/');
        //    if (parts.Length > 1 && int.TryParse(parts[1], out int extractedSubNo))
        //    {
        //        return extractedSubNo == subNo;
        //    }
        //    return false;
        //}

        //private bool PlotNoMatches(string matrikkelnummertekst, int? plotNo)
        //{
        //    if (!plotNo.HasValue)
        //    {
        //        return true; // No plotNo filter applied
        //    }

        //    // Example logic: Extract plotNo from matrikkelnummertekst and compare
        //    var parts = matrikkelnummertekst.Split('/');
        //    if (parts.Length > 2 && int.TryParse(parts[2], out int extractedPlotNo))
        //    {
        //        return extractedPlotNo == plotNo.Value;
        //    }
        //    return false;
        //}
    }

    public class FeatureDto
    {
        public string Type { get; set; } = "Feature";
        public GeometryDto Geometry { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public Guid UuidFeatureDrawn { get; set; }
        public string IconType { get; set; }
    }

    //public class GeometryDto
    //{
    //    public string Type { get; set; }
    //    public List<List<List<double>>> Coordinates { get; set; }
    //    //public double[][] Coordinates { get; set; }
    //}


    public class GeometryDto
    {
        public string type { get; set; }
        public object coordinates { get; set; }
    }

    public class PointDto : GeometryDto
    {
        public new double[] coordinates { get; set; }
    }

    public class LineStringDto : GeometryDto
    {
        public new double[][] coordinates { get; set; }
    }

    public class PolygonDto : GeometryDto
    {
        public new double[][][] coordinates { get; set; }
    }

}

//foreach (var feature in featureCollection.Features)
//{
//    var geoJsonReader = new GeoJsonReader();
//    //var geometryJson = feature.geometry.ToString();
//    //var geometry = geoJsonReader.Read<Geometry>(geometryJson);
//    //var featuredrawn = new Featuredrawn
//    //{
//    //    Geometry = geometry,
//    //    Properties = feature.properties.ToString(),
//    //   // Style = feature.Properties["style"] // Assuming style is part of properties
//    //};

//   // list.Add(featuredrawn);
//}
//await _context.Featuredrawns.AddRangeAsync(list);
//var featureCollection = System.Text.Json.JsonSerializer.Serialize(geoJsonFeatureCollection);
////  var serializer = new GeoJsonSerializer();
////var featureCollection = serializer.Deserialize<FeatureCollection>(new JsonTextReader(new StringReader(geojsonString)));
//var geoJsonReader = new GeoJsonReader();
//var list = new List<Featuredrawn>();
//foreach (var feature in geoJsonFeatureCollection.features)
//{
//    var geometryJson = feature["geometry"].ToString();
//    var geometry = geoJsonReader.Read<Geometry>(geometryJson);

//    var featuredrawn = new Featuredrawn
//    {
//        Geom = geometry//(Geometry)feature["geometry"]
//    };
//    list.Add(featuredrawn);
//}
