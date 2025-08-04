using ViltrapportenApi.Data.CustomModels;

namespace ViltrapportenApi.Modal
{
    public class LandOwnersObj
    {
        public LandOwnersObj()
        {
            LandOwner = new List<OwnerInfo>();
        }

        public float ForestArea { get; set; }
        public float MountainArea { get; set; }
        public float AgricultureArea { get; set; }
        public int LandsCount { get; set; }
        public float UnitTotalArea { get; set; }
        public int OwnersCount { get; set; }
        public IList<OwnerInfo> LandOwner { get; set; }
    }
}
