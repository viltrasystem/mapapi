using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
//using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

//using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using ViltrapportenApi.Data.MapModels;

public class GeoJsonConverter
{
    /// deleted due to remove Hoydekurve
    //public string ConvertHoydekurveToGeoJson(IEnumerable<Hoydekurve> hoydekurveList)
    //{
    //    var featureCollection = CreateFeatureCollection(hoydekurveList);
    //    return JsonConvert.SerializeObject(featureCollection);
    //}

    //private FeatureCollection CreateFeatureCollection(IEnumerable<Hoydekurve> hoydekurveList)
    //{
    //    var features = new List<Feature>();

    //    foreach (var hoydekurve in hoydekurveList)
    //    {
    //        var lineString = ConvertGeometryToLineString(hoydekurve.Senterlinje);
    //        if (lineString != null)
    //        {
    //            var feature = CreateFeatureFromHoydekurve(hoydekurve, lineString);
    //            features.Add(feature);
    //        }
    //    }

    //    return new FeatureCollection(features);
    //}

    //private Feature CreateFeatureFromHoydekurve(Hoydekurve hoydekurve, LineString lineString)
    //{
    //    var feature = new Feature(lineString);
    //    feature.Properties.Add("Objid", hoydekurve.Objid);
    //    feature.Properties.Add("Objtype", hoydekurve.Objtype);
    //    feature.Properties.Add("Medium", hoydekurve.Medium);
    //    feature.Properties.Add("Oppdateringsdato", hoydekurve.Oppdateringsdato?.ToString("yyyy-MM-dd"));
    //    feature.Properties.Add("Malemetode", hoydekurve.Malemetode);
    //    feature.Properties.Add("Noyaktighet", hoydekurve.Noyaktighet);
    //    feature.Properties.Add("Hoyde", hoydekurve.Hoyde);

    //    return feature;
    //}
    /// deleted due to remove Hoydekurve
    public string ConvertEiendomsgrenseToGeoJson(IEnumerable<Eiendomsgrense> eiendomsgrenseList)
    {
        var featureCollection = CreateEiendomsgrenseFeatureCollection(eiendomsgrenseList);
        return JsonConvert.SerializeObject(featureCollection);
    }

    private FeatureCollection CreateEiendomsgrenseFeatureCollection(IEnumerable<Eiendomsgrense> eiendomsgrenseList)
    {
        var features = new List<Feature>();

        foreach (var eiendomsgrense in eiendomsgrenseList)
        {
            var lineString = ConvertGeometryToLineString(eiendomsgrense.Grense);
            if (lineString != null)
            {
                var feature = CreateFeatureFromEiendomsgrense(eiendomsgrense, lineString);
                features.Add(feature);
            }
        }

        return new FeatureCollection(features);
    }

    private Feature CreateFeatureFromEiendomsgrense(Eiendomsgrense eiendomsgrense, LineString lineString)
    {
        var feature = new Feature(lineString);
        feature.Properties.Add("Objid", eiendomsgrense.Objid);
        feature.Properties.Add("Objtype", eiendomsgrense.Objtype);
        feature.Properties.Add("Teiggrenseid", eiendomsgrense.Teiggrenseid);
        feature.Properties.Add("Navnerom", eiendomsgrense.Navnerom);
        feature.Properties.Add("Versjonid", eiendomsgrense.Versjonid);
        feature.Properties.Add("Datafangstdato", eiendomsgrense.Datafangstdato);
        feature.Properties.Add("Oppdateringsdato", eiendomsgrense.Oppdateringsdato);
        feature.Properties.Add("Datauttaksdato", eiendomsgrense.Datauttaksdato);
        feature.Properties.Add("Malemetode", eiendomsgrense.Malemetode);
        feature.Properties.Add("Noyaktighet", eiendomsgrense.Noyaktighet);
        feature.Properties.Add("Administrativgrense", eiendomsgrense.Administrativgrense);
        feature.Properties.Add("Omtvistet", eiendomsgrense.Omtvistet);
        feature.Properties.Add("Noyaktighetsklasse", eiendomsgrense.Noyaktighetsklasse);
        feature.Properties.Add("Uuidteiggrense", eiendomsgrense.Uuidteiggrense);
        feature.Properties.Add("Folgerterrengdetalj", eiendomsgrense.Folgerterrengdetalj);


        return feature;
    }

    private GeoJSON.Net.Geometry.LineString ConvertGeometryToLineString(NetTopologySuite.Geometries.Geometry geometry)
    {
        if (geometry is NetTopologySuite.Geometries.LineString lineString)
        {
            var coordinates = lineString.Coordinates
                .Select(point => new Position(point.Y, point.X)) // (latitude, longitude) format
                .ToList();

            return new LineString(coordinates);
        }

        // Unsupported geometry type; return null or an empty LineString
        return null;
    }

    //public string ConvertTeigToGeoJson(Teig teig)
    //{
    //    var featureCollection = CreateTeigFeatureCollection(teig);
    //    return JsonConvert.SerializeObject(featureCollection);
    //}

    //private FeatureCollection CreateTeigFeatureCollection(Teig teig)
    //{
    //    var features = new List<Feature>();

    //        var polygon = ConvertGeometryToPolygon(teig.Omrade);
    //        if (polygon != null)
    //        {
    //            var feature = CreateFeatureFromTeig(teig, polygon);
    //            features.Add(feature);
    //        }

    //    return new FeatureCollection(features);
    //}

    //private Feature CreateFeatureFromTeig(Teig teig, Polygon polygon)
    //{
    //    var feature = new Feature(polygon);
    //    feature.Properties.Add("Objid", teig.Objid);
    //    feature.Properties.Add("Objtype", teig.Objtype);
    //    feature.Properties.Add("Teigid", teig.Teigid);
    //    feature.Properties.Add("Navnerom", teig.Navnerom);
    //    feature.Properties.Add("Versjonid", teig.Versjonid);
    //    feature.Properties.Add("Datafangstdato", teig.Datafangstdato);
    //    feature.Properties.Add("Oppdateringsdato", teig.Oppdateringsdato);
    //    feature.Properties.Add("Datauttaksdato", teig.Datauttaksdato);
    //    feature.Properties.Add("Malemetode", teig.Malemetode);
    //    feature.Properties.Add("Noyaktighet", teig.Noyaktighet);
    //    feature.Properties.Add("Representasjonspunkt", teig.Representasjonspunkt);
    //    feature.Properties.Add("Omrade", teig.Omrade);
    //    feature.Properties.Add("Kommunenummer", teig.Kommunenummer);
    //    feature.Properties.Add("Kommunenavn", teig.Kommunenavn);
    //    feature.Properties.Add("Matrikkelnummertekst", teig.Matrikkelnummertekst);
    //    feature.Properties.Add("Teigmedflerematrikkelenheter", teig.Teigmedflerematrikkelenheter);
    //    feature.Properties.Add("Tvist", teig.Tvist);
    //    feature.Properties.Add("Uregistrertjordsameie", teig.Uregistrertjordsameie);
    //    feature.Properties.Add("Avklarteiere", teig.Avklarteiere);
    //    feature.Properties.Add("Lagretberegnetareal", teig.Lagretberegnetareal);
    //    feature.Properties.Add("Arealmerknadtekst", teig.Arealmerknadtekst);
    //    feature.Properties.Add("Noyaktighetsklasseteig", teig.Noyaktighetsklasseteig);
    //    feature.Properties.Add("Uuidteig", teig.Uuidteig);

    //    return feature;
    //}

    
    //private NetTopologySuite.Geometries.Polygon ConvertGeometryToPolygon(NetTopologySuite.Geometries.Geometry geometryData)
    //{ // Create a WKT reader
    //    var writer = new GeoJsonWriter();

    //    // Convert the geometry to GeoJSON
    //    var geoJson = writer.Write(geometryData);
       
    //    var reader = new WKTReader();
    //    NetTopologySuite.Geometries.Geometry geometry = reader.Read(geoJson);

    //    // Check if the geometry is a polygon
    //    if (geometry is NetTopologySuite.Geometries.Polygon)
    //    {
    //        // Convert the geometry to a polygon
    //        NetTopologySuite.Geometries.Polygon polygon = (NetTopologySuite.Geometries.Polygon)geometry;
    //    }


    //    // Unsupported geometry type; return null or an empty LineString
    //    return null;
    //}
}

    //private GeoJSON.Net.Geometry.LineString ConvertGeometryToLineString(NetTopologySuite.Geometries.Geometry geometry)
    //{
    //    if (geometry is NetTopologySuite.Geometries.LineString lineString)
    //    {
    //        var coordinates = lineString.Coordinates
    //            .Select(point => new Position(point.Y, point.X)) // (latitude, longitude) format
    //            .ToList();

    //        return new LineString(coordinates);
    //    }

    //    // Unsupported geometry type; return null or an empty LineString
    //    return null;
    //}















































//using System.Collections.Generic;
//using GeoJSON.Net.Feature;
//using GeoJSON.Net.Geometry;

//namespace ViltrapportenApi.Modal
//{

//public class GeoJsonConverter
//{
//    public string ConvertToGeoJson(List<MyEntity> entities)
//    {
//        var features = new List<Feature>();

//        foreach (var entity in entities)
//        {
//            var geometry = new Point(new GeographicPosition(entity.Senterlinje.Y, entity.Senterlinje.X));
//            var properties = new Dictionary<string, object>
//            {
//                { "objid", entity.Objid },
//                // Add other properties as needed
//            };

//            var feature = new Feature(geometry, properties);
//            features.Add(feature);
//        }

//        var featureCollection = new FeatureCollection(features);
//        var geoJson = featureCollection.Serialize();

//        return geoJson;
//    }
//}
//}


