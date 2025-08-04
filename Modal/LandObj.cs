
namespace ViltrapportenApi.Modal
{
    public class LandObj
    {
        public LandObj()
        {
            this.Lands = new List<LandInfo>();
        }

        public int TotalLandsCount { get; set; }
        public int TotalSharedLandsCount { get; set; }
        public float TotalForestArea { get; set; }
        public float TotalMountainArea { get; set; }
        public float TotalAgricultureArea { get; set; }
        public float TotalArea { get; set; }
        public List<LandInfo> Lands { get; set; }
    }
}
