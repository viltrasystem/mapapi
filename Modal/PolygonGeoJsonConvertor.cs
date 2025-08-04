using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
//using NetTopologySuite.Features;
//using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;
using NetTopologySuite.IO;

//using NetTopologySuite.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using ViltrapportenApi.Data.MapModels;
using static ViltrapportenApi.Modal.ApiRoutes;

namespace ViltrapportenApi.Modal
{
    public class PolygonGeoJsonConvertor
    {

        public string ConvertTeigToGeoJson(List<LandTeigDTO> teigList, IList<LandUnitInfo> landUnits, int? landId, int? mainNo, int? subNo, int? plotNo)
        {
            var featureCollection = CreateTeigFeatureCollection(teigList,landUnits, landId, mainNo, subNo, plotNo);
            return JsonConvert.SerializeObject(featureCollection);
        }

        private FeatureCollection CreateTeigFeatureCollection(List<LandTeigDTO> teigList, IList<LandUnitInfo> landUnits, int? landId, int? mainNo, int? subNo, int? plotNo)
        {
            var features = new List<Feature>();

            foreach (var teig in teigList)
            {
                var polygon = ConvertGeometryToPolygon(teig.Geometry);
                if (polygon != null)
                {
                    var polygonJson = ConvertToGeoJsonPolygon(polygon);
                    var feature = CreateFeatureFromTeig(teig, polygonJson, landUnits, landId, mainNo, subNo, plotNo);
                    features.Add(feature);
                }
            }

            return new FeatureCollection(features);
        }


        static Polygon ConvertToGeoJsonPolygon(NetTopologySuite.Geometries.Polygon ntsPolygon)
        {
            var writer = new GeoJsonWriter();
            string geoJsonString = writer.Write(ntsPolygon);

            // Deserialize GeoJSON to GeoJSON.Net.Geometry.Polygon
            var geoJsonPolygon = JsonConvert.DeserializeObject<GeoJSON.Net.Geometry.Polygon>(geoJsonString);

            return geoJsonPolygon;
        }


        private Feature CreateFeatureFromTeig(LandTeigDTO teig, Polygon polygon, IList<LandUnitInfo> landUnits, int? landId, int? mainNo, int? subNo, int? plotNo)
        {
            var parts = new List<string>();
            if (mainNo.HasValue)
            {
                parts.Add(mainNo.Value.ToString());
            }

            if (subNo.HasValue)
            {
                parts.Add(subNo.Value.ToString());
            }

            if (plotNo.HasValue)
            {
                parts.Add(plotNo.Value.ToString());
            }

            string mainSubPlotNo = string.Join("/", parts);
            var uuidLandDrawn = teig.UuidLandDrawn == Guid.Empty ? Guid.NewGuid() : teig.UuidLandDrawn;

            var feature = new Feature(polygon);
            feature.Properties.Add("LandId", landId);
            feature.Properties.Add("LandDrawnId", teig.LandDrawnId);
            feature.Properties.Add("UuidLandDrawn", uuidLandDrawn);
            feature.Properties.Add("TeigId", teig.TeigId);
            feature.Properties.Add("MunicipalityNo", teig.MunicipalityNo);
            feature.Properties.Add("MainNo", mainNo);
            feature.Properties.Add("SubNo", subNo);
            feature.Properties.Add("PlotNo", plotNo);
            feature.Properties.Add("MunicipalityName", teig.MunicipalityName);
            feature.Properties.Add("MainSubPlotNo", mainSubPlotNo);
            feature.Properties.Add("CreatedBy", teig.CreatedBy);
            feature.Properties.Add("EditedBy", teig.EditedBy);
            feature.Properties.Add("LandUnits", landUnits);
            feature.Properties.Add("Matrikkelnummertekst", teig.MainNo);
            feature.Properties.Add("Properties", teig.Properties);

            return feature;
        }


        private NetTopologySuite.Geometries.Polygon ConvertGeometryToPolygon(NetTopologySuite.Geometries.Geometry geometryData)
        {
            // Check if the geometry is a polygon

            if (geometryData is NetTopologySuite.Geometries.Polygon)
            {
                // Convert the geometry to a polygon
                NetTopologySuite.Geometries.Polygon polygon = (NetTopologySuite.Geometries.Polygon)geometryData;
                return polygon;
            }


            // Unsupported geometry type; return null or an empty LineString
            return null;
        }


        //public string ConvertLandToGeoJson(LandDrawn land, IList<LandUnitInfo> landUnits, int? landId, int? mainNo, int? subNo, int? plotNo)
        //{
        //    var featureCollection = CreateLandFeatureCollection(land, landUnits, landId, mainNo, subNo, plotNo);
        //    return JsonConvert.SerializeObject(featureCollection);
        //}

        //private FeatureCollection CreateLandFeatureCollection(LandDrawn land, IList<LandUnitInfo> landUnits, int? landId, int? mainNo, int? subNo, int? plotNo)
        //{
        //    var features = new List<Feature>();


        //    var polygon = ConvertGeometryToPolygon(land.Geometry);
        //    if (polygon != null)
        //    {
        //        var polygonJson = ConvertToGeoJsonPolygon(polygon);
        //        var feature = CreateFeatureFromLand(land, polygonJson, landUnits, landId, mainNo, subNo, plotNo);
        //        features.Add(feature);
        //    }

        //    return new FeatureCollection(features);
        //}

        //private Feature CreateFeatureFromLand(LandDrawn land, Polygon polygon, IList<LandUnitInfo> landUnits, int? landId, int? mainNo, int? subNo, int? plotNo)
        //{
        //    var parts = new List<string>();
        //    if (mainNo.HasValue)
        //    {
        //        parts.Add(mainNo.Value.ToString());
        //    }

        //    if (subNo.HasValue)
        //    {
        //        parts.Add(subNo.Value.ToString());
        //    }

        //    if (plotNo.HasValue)
        //    {
        //        parts.Add(plotNo.Value.ToString());
        //    }

        //    string mainSubPlotNo = string.Join("/", parts);

        //    var feature = new Feature(polygon);
        //    feature.Properties.Add("LandId", landId);
        //    feature.Properties.Add("MainNo", mainNo);
        //    feature.Properties.Add("SubNo", subNo);
        //    feature.Properties.Add("PlotNo", plotNo);
        //    feature.Properties.Add("LandDrawnId", land.LandDrawnId);
        //    feature.Properties.Add("UuidLandDrawn", land.UuidLandDrawn);
        //    feature.Properties.Add("TeigId", land.TeigId);
        //    feature.Properties.Add("MunicipalityNo", land.MunicipalityNo);
        //    feature.Properties.Add("MunicipalityName", land.MunicipalityName);
        //    feature.Properties.Add("MainSubPlotNo", mainSubPlotNo);
        //    feature.Properties.Add("EditedBy", land.EditedBy);
        //    feature.Properties.Add("LandUnits", landUnits);
        //    return feature;
        //}


        // feature.Properties.Add("Representasjonspunkt", teig.Representasjonspunkt);
        //feature.Properties.Add("Omrade", teig.Omrade);
        //feature.Properties.Add("Objid", teig.Objid);
        //feature.Properties.Add("Objtype", teig.Objtype);
        //feature.Properties.Add("Navnerom", teig.Navnerom);
        //feature.Properties.Add("Versjonid", teig.Versjonid);
        //feature.Properties.Add("Datafangstdato", teig.Datafangstdato);
        //feature.Properties.Add("Oppdateringsdato", teig.Oppdateringsdato);
        //feature.Properties.Add("Datauttaksdato", teig.Datauttaksdato);
        //feature.Properties.Add("Malemetode", teig.Malemetode);
        //feature.Properties.Add("Noyaktighet", teig.Noyaktighet);
        //feature.Properties.Add("Teigmedflerematrikkelenheter", teig.Teigmedflerematrikkelenheter);
        //feature.Properties.Add("Tvist", teig.Tvist);
        //feature.Properties.Add("Uregistrertjordsameie", teig.Uregistrertjordsameie);
        //feature.Properties.Add("Avklarteiere", teig.Avklarteiere);
        //feature.Properties.Add("Lagretberegnetareal", teig.Lagretberegnetareal);
        //feature.Properties.Add("Arealmerknadtekst", teig.Arealmerknadtekst);
        //feature.Properties.Add("Noyaktighetsklasseteig", teig.Noyaktighetsklasseteig);
        //feature.Properties.Add("Uuidteig", teig.Uuidteig);


        // Create a WKT reader
        //var writer = new NetTopologySuite.IO.GeoJsonWriter();

        //// Convert the geometry to GeoJSON
        //var geoJson = writer.Write(geometryData);

        //var reader = new NetTopologySuite.IO.WKTReader();
        //NetTopologySuite.Geometries.Geometry geometry = reader.Read(geoJson);


        //    static Polygon ConvertToGeoJsonPolygon(NetTopologySuite.Geometries.Polygon ntsPolygon)
        //    {
        //        var exteriorRing = ntsPolygon.ExteriorRing.Coordinates;
        //        var coordinates = new List<IPosition>();

        //        // Add exterior ring coordinates to the list
        //        foreach (var coordinate in exteriorRing)
        //        {
        //            coordinates.Add(new Position(coordinate.Y, coordinate.X));
        //        }

        //        // Ensure closed loop (add the first coordinate to the end of the list)
        //        coordinates.Add(new Position(exteriorRing[0].Y, exteriorRing[0].X));

        //        // Create a GeoJSON polygon
        //        var geoJsonPolygon = new Polygon(new List<LineString>
        //{
        //    new LineString(coordinates)
        //});

        //        return geoJsonPolygon;
        //        //var coordinates = new List<IPosition>();
        //        //foreach (var coordinate in ntsPolygon.Coordinates)
        //        //{
        //        //    coordinates.Add(new Position(coordinate.Y, coordinate.X));
        //        //}

        //        //coordinates.Add(new Position(ntsPolygon.Coordinates[0].Y, ntsPolygon.Coordinates[0].X));

        //        //// Log coordinates for debugging
        //        //Console.WriteLine("Coordinates:");
        //        //foreach (var coord in coordinates)
        //        //{
        //        //    Console.WriteLine($"Lat: {coord.Latitude}, Lon: {coord.Longitude}");
        //        //}
        //        //// Create a GeoJSON polygon
        //        //var geoJsonPolygon = new Polygon(new List<LineString>
        //        //    {
        //        //        new LineString(coordinates)
        //        //    });

        //        //return geoJsonPolygon;
        //    }

        //    static Polygon ConvertToGeoJsonPolygon(NetTopologySuite.Geometries.Polygon ntsPolygon)
        //    {
        //        var exteriorRing = ntsPolygon.ExteriorRing.Coordinates;
        //        var interiorRings = ntsPolygon.InteriorRings;

        //        var coordinates = new List<IPosition>();

        //        // Add exterior ring coordinates to the list
        //        foreach (var coordinate in exteriorRing)
        //        {
        //            coordinates.Add(new Position(coordinate.Y, coordinate.X));
        //        }

        //        // Ensure closed loop for the exterior ring
        //        coordinates.Add(new Position(exteriorRing[0].Y, exteriorRing[0].X));

        //        // Add interior ring coordinates to the list (if any)
        //        foreach (var interiorRing in interiorRings)
        //        {
        //            var interiorCoordinates = new List<IPosition>();
        //            foreach (var coordinate in interiorRing.Coordinates)
        //            {
        //                interiorCoordinates.Add(new Position(coordinate.Y, coordinate.X));
        //            }
        //            // Ensure closed loop for the interior ring
        //            interiorCoordinates.Add(new Position(interiorRing.Coordinates[0].Y, interiorRing.Coordinates[0].X));

        //            // Add the interior ring to the list of coordinates
        //            coordinates.AddRange(interiorCoordinates);
        //        }

        //        // Create a GeoJSON polygon
        //        var geoJsonPolygon = new Polygon(new List<LineString>
        //{
        //    new LineString(coordinates)
        //});

        //        return geoJsonPolygon;
        //    }

    }
}


//using GeoJSON.Net.Feature;
//using GeoJSON.Net.Geometry;
////using NetTopologySuite.Features;
////using NetTopologySuite.Geometries;
//using NetTopologySuite.Geometries.Utilities;
////using NetTopologySuite.IO;
//using Newtonsoft.Json;
//using ViltrapportenApi.Data.MapModels;

//namespace ViltrapportenApi.Modal
//{
//    public class PolygonGeoJsonConvertor
//    {

//        public string ConvertTeigToGeoJson(Teig teig)
//        {
//            var featureCollection = CreateTeigFeatureCollection(teig);
//            return JsonConvert.SerializeObject(featureCollection);
//        }

//        private FeatureCollection CreateTeigFeatureCollection(Teig teig)
//        {
//            var features = new List<Feature>();

//            var polygon = ConvertGeometryToPolygon(teig.Omrade);
//            if (polygon != null)
//            {
//                var polygonJson = ConvertToGeoJsonPolygon(polygon);
//                var feature = CreateFeatureFromTeig(teig, polygonJson);
//                features.Add(feature);
//            }

//            return new FeatureCollection(features);
//        }

//        static Polygon ConvertToGeoJsonPolygon(NetTopologySuite.Geometries.Polygon ntsPolygon)
//        {
//            var coordinates = new List<IPosition>();
//            foreach (var coordinate in ntsPolygon.Coordinates)
//            {
//                coordinates.Add(new Position(coordinate.Y, coordinate.X));
//            }

//            // Create a GeoJSON polygon
//            var geoJsonPolygon = new Polygon(new List<LineString>
//{
//    new LineString(coordinates)
//});
//            return geoJsonPolygon;
//            //var exteriorRingCoordinates = new List<IPosition>();
//            //foreach (var coordinate in ntsPolygon.ExteriorRing.Coordinates)
//            //{
//            //    exteriorRingCoordinates.Add(new Position(coordinate.Y, coordinate.X));
//            //}

//            //var interiorRings = new List<LineString>();
//            //foreach (var interiorRing in ntsPolygon.InteriorRings)
//            //{
//            //    var interiorRingCoordinates = new List<IPosition>();
//            //    foreach (var coordinate in interiorRing.Coordinates)
//            //    {
//            //        interiorRingCoordinates.Add(new Position(coordinate.Y, coordinate.X));
//            //    }
//            //    interiorRings.Add(new LineString(interiorRingCoordinates));
//            //}

//            //var geoJsonPolygon = new Polygon(new LineString(exteriorRingCoordinates));
//            //foreach (var ring in interiorRings)
//            //{
//            //    geoJsonPolygon.AppendRing(ring);
//            //}

//            //return geoJsonPolygon;
//        }

//        private Feature CreateFeatureFromTeig(Teig teig, Polygon polygon)
//        {
//            var feature = new Feature(polygon);
//            feature.Properties.Add("Objid", teig.Objid);
//            feature.Properties.Add("Objtype", teig.Objtype);
//            feature.Properties.Add("Teigid", teig.Teigid);
//            feature.Properties.Add("Navnerom", teig.Navnerom);
//            feature.Properties.Add("Versjonid", teig.Versjonid);
//            feature.Properties.Add("Datafangstdato", teig.Datafangstdato);
//            feature.Properties.Add("Oppdateringsdato", teig.Oppdateringsdato);
//            feature.Properties.Add("Datauttaksdato", teig.Datauttaksdato);
//            feature.Properties.Add("Malemetode", teig.Malemetode);
//            feature.Properties.Add("Noyaktighet", teig.Noyaktighet);
//            feature.Properties.Add("Representasjonspunkt", teig.Representasjonspunkt);
//            feature.Properties.Add("Omrade", teig.Omrade);
//            feature.Properties.Add("Kommunenummer", teig.Kommunenummer);
//            feature.Properties.Add("Kommunenavn", teig.Kommunenavn);
//            feature.Properties.Add("Matrikkelnummertekst", teig.Matrikkelnummertekst);
//            feature.Properties.Add("Teigmedflerematrikkelenheter", teig.Teigmedflerematrikkelenheter);
//            feature.Properties.Add("Tvist", teig.Tvist);
//            feature.Properties.Add("Uregistrertjordsameie", teig.Uregistrertjordsameie);
//            feature.Properties.Add("Avklarteiere", teig.Avklarteiere);
//            feature.Properties.Add("Lagretberegnetareal", teig.Lagretberegnetareal);
//            feature.Properties.Add("Arealmerknadtekst", teig.Arealmerknadtekst);
//            feature.Properties.Add("Noyaktighetsklasseteig", teig.Noyaktighetsklasseteig);
//            feature.Properties.Add("Uuidteig", teig.Uuidteig);

//            return feature;
//        }


//        private NetTopologySuite.Geometries.Polygon ConvertGeometryToPolygon(NetTopologySuite.Geometries.Geometry geometryData)
//        { // Create a WKT reader
//            //var writer = new NetTopologySuite.IO.GeoJsonWriter();

//            //// Convert the geometry to GeoJSON
//            //var geoJson = writer.Write(geometryData);

//            //var reader = new NetTopologySuite.IO.WKTReader();
//            //NetTopologySuite.Geometries.Geometry geometry = reader.Read(geoJson);

//            // Check if the geometry is a polygon

//            if (geometryData is NetTopologySuite.Geometries.Polygon)
//            {
//                // Convert the geometry to a polygon
//                NetTopologySuite.Geometries.Polygon polygon = (NetTopologySuite.Geometries.Polygon)geometryData;
//                return polygon;
//            }


//            // Unsupported geometry type; return null or an empty LineString
//            return null;
//        }

//        //public ICollection<Geometry> ParseGeoShapes(string data)
//        //{
//        //    var reader = new GeoJsonReader();
//        //    var featureCollection = reader.Read<GeoJSON.Net.Feature.FeatureCollection>(data);

//        //    var shapes = new List<Geometry>();

//        //    foreach (var feature in featureCollection)
//        //        shapes.AddRange(Extract(feature));
//        //}

//        //public IEnumerable<Geometry> Extract(IFeature feature)
//        //{
//        //    var extract = new List<Geometry>();

//        //    new GeometryExtracter<Polygon>(extract).Filter(feature.Geometry);

//        //    return extract;
//        //}

//        //public string ConvertTeigToGeoJson(Teig teig)
//        //{
//        //    var featureCollection = CreateFeatureCollection(teig);
//        //    return JsonConvert.SerializeObject(featureCollection);
//        //}

//        //private FeatureCollection CreateFeatureCollection(Teig teig)
//        //{
//        //    var features = new List<Feature>();

//        //    //foreach (var hoydekurve in hoydekurveList)
//        //    //{
//        //        var polygon = ConvertGeometryToPolygon(teig.Omrade);
//        //        if (polygon != null)
//        //        {
//        //            var feature = CreateFeatureFromTeig(teig, polygon);
//        //            features.Add(feature);
//        //        }
//        //    //}

//        //    return new FeatureCollection(features);
//        //}

//        //private Feature CreateFeatureFromTeig(Teig teig, GeoJSON.Net.Geometry.Polygon polygon)
//        //{
//        //    var feature = new Feature(polygon);

//        //    feature.Properties.Add("Objid", teig.Objid);
//        //    feature.Properties.Add("Objtype", teig.Objtype);
//        //    feature.Properties.Add("Teigid", teig.Teigid);
//        //    feature.Properties.Add("Navnerom", teig.Navnerom);
//        //    feature.Properties.Add("Versjonid", teig.Versjonid);
//        //    feature.Properties.Add("Datafangstdato", teig.Datafangstdato);
//        //    feature.Properties.Add("Oppdateringsdato", teig.Oppdateringsdato);
//        //    feature.Properties.Add("Datauttaksdato", teig.Datauttaksdato);
//        //    feature.Properties.Add("Malemetode", teig.Malemetode);
//        //    feature.Properties.Add("Noyaktighet", teig.Noyaktighet);
//        //    feature.Properties.Add("Representasjonspunkt", teig.Representasjonspunkt);
//        //    feature.Properties.Add("Omrade", teig.Omrade);
//        //    feature.Properties.Add("Kommunenummer", teig.Kommunenummer);
//        //    feature.Properties.Add("Kommunenavn", teig.Kommunenavn);
//        //    feature.Properties.Add("Matrikkelnummertekst", teig.Matrikkelnummertekst);
//        //    feature.Properties.Add("Teigmedflerematrikkelenheter", teig.Teigmedflerematrikkelenheter);
//        //    feature.Properties.Add("Tvist", teig.Tvist);
//        //    feature.Properties.Add("Uregistrertjordsameie", teig.Uregistrertjordsameie);
//        //    feature.Properties.Add("Avklarteiere", teig.Avklarteiere);
//        //    feature.Properties.Add("Lagretberegnetareal", teig.Lagretberegnetareal);
//        //    feature.Properties.Add("Arealmerknadtekst", teig.Arealmerknadtekst);
//        //    feature.Properties.Add("Noyaktighetsklasseteig", teig.Noyaktighetsklasseteig);
//        //    feature.Properties.Add("Uuidteig", teig.Uuidteig);

//        //    return feature;
//        //}

//        //internal string ConvertGeometryToPolygon(Geometry omrade)
//        //{
//        //    var writer = new GeoJsonWriter();

//        //    // Convert the geometry to GeoJSON
//        //    var geoJson = writer.Write(omrade);
//        //            return geoJson;
//        //    // Print or use the GeoJSON string as needed
//        //}
//    }
//}

