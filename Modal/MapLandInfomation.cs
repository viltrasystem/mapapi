
namespace ViltrapportenApi.Modal
{
    public class MapLandInformation
    {
        public string? MapGeoJson { get; set; }
        public List<LandInformation> LandInformations { get; set; } = new List<LandInformation>();
    }
}
