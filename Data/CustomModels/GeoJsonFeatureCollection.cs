using NetTopologySuite.Geometries;

namespace ViltrapportenApi.Data.CustomModels
{
    //public class GeoJsonFeatureCollection
    //{
    //    public string type { get; set; }
    //    public List<Dictionary<string, object>> Features { get; set; }
    //}
    public class DrawFeature
    {
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public IDictionary<string, object> properties { get; set; }

        public DrawFeature(IDictionary<string, object> properties)
        {
           properties = properties;
        }
    }

    public class DrawFeatureCollection
    {
        public string type { get; set; }
        public List<Dictionary<string, object>> Features { get; set; }
        //public List<DrawFeature> features { get; set; }
    }
    
    public class DeleteLandLayerReq
    {
        public int LandDrawnId { get; set; }
        public int TeigId { get; set; }
        public int UserId { get; set; }
    }

}
