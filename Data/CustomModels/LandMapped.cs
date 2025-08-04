namespace ViltrapportenApi.Data.CustomModels
{
    public class LandMapped
    {
        public int LandId { get; set; }
        public string Municipality { get; set; }
        public string MainNo { get; set; }
        public string SubNo { get; set; }
        public string PlotNo { get; set; }
        public float AreaInForest { get; set; }
        public float AreaInMountain { get; set; }
        public float AreaInAgriculture { get; set; }
        public float TotalArea { get; set; }
        public string Notes { get; set; }
        public int OwnershipTypeId { get; set; }
        public int ContactOwnerLandId { get; set; }
        public int LandOwnerId { get; set; }
        public int SystemUserId { get; set; }
        public int DnnUserId { get; set; }
        public string DisplayName { get; set; }
        public string OwnershipType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public int UnitId { get; set; }
        public string Unit { get; set; }
        public int LandTypeId { get; set; }
        public int ParentUId { get; set; }
        public string ParentUnit { get; set; }
    }
}
