using ViltrapportenApi.Data.SystemModels;

namespace ViltrapportenApi.Modal
{
    public class LandInfo
    {
        public LandInfo()
        {
            this.LandOwners = new List<LandOwnerInfo>();
            this.ArchivedLandowners = new List<LandOwnerInfo>();
            this.LandUnits = new List<LandUnitInfo>();
            this.LandOwnershipTypes = new List<LandOwnershipType>();
        }

        public int LandId { get; set; }

        public string Municipality { get; set; }
        public string MainNo { get; set; }
        public string SubNo { get; set; }
        public string PlotNo { get; set; }
        public int NoOfReferencedLands { get; set; }

        public float AreaInForest { get; set; }
        public float AreaInMountain { get; set; }
        public float AreaInAgriculture { get; set; }
        public float TotalArea { get; set; }

        public string Notes { get; set; }

        public int OwnershipTypeId { get; set; }
        public string OwnershipType { get; set; }

        public int LandOwnerId { get; set; }
        public string DisplayName { get; set; }
        public int CreatedBy { get; set; } // for form filed
        public string MapGeoJson  { get; set; } = null!;

        public IList<LandOwnerInfo> ArchivedLandowners { get; set; }// for form filed
        public IList<LandOwnerInfo> LandOwners { get; set; }
        public IList<LandUnitInfo> LandUnits { get; set; }
        public IList<LandOwnershipType> LandOwnershipTypes { get; set; }
    }
}
