using ViltrapportenApi.Data.SystemModels;

namespace ViltrapportenApi.Modal
{
    public class LandInformation
    {
        public int LandId { get; set; }

        public string Municipality { get; set; } = null!;

        public string MainNo { get; set; } = null!;

        public string SubNo { get; set; } = null!;

        public string PlotNo { get; set; } = null!;

        public int OwnershipTypeId { get; set; }

        public float AreaInForest { get; set; }

        public float AreaInMountain { get; set; }

        public float TotalArea { get; set; }

        public string? Notes { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public float AreaInAgriculture { get; set; }

        public string MunicipalityName { get; set; } = null!;

        public virtual IList<LandOwnerInformation> LandOwners { get; set; } = new List<LandOwnerInformation>();

        public virtual IList<LandUnitInfo> LandUnits { get; set; } = new List<LandUnitInfo>();

        public virtual LandownershipType OwnershipType { get; set; } = null!;
    }
}
