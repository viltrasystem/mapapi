namespace ViltrapportenApi.Modal
{

    public class OwnerInfo()
    {
        //public OwnerInfo()
        //{
        //    this.Owners = new List<OwnerInfo>();
        //}

        public bool IsSharedLand { get; set; }
        public int LandId { get; set; }
        public string? LandIdListStr { get; set; }
        public int ContactOwnerLandId { get; set; }
        public int LandOwnerId { get; set; }
        public int SystemUserId { get; set; }
        public string? DisplayName { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public string? AddLine1 { get; set; }
        public string? AddLine2 { get; set; }
        public string? AddCity { get; set; }
        public string? BankAccountNo { get; set; }
        public string? Notes { get; set; }
        public string? LandNote { get; set; }
        public string? Unit { get; set; }
        public int NoOfOccurrences { get; set; }
        public float ForestArea { get; set; }
        public float MountainArea { get; set; }
        public float AgricultureArea { get; set; }
        public double LandOwnerForestShare { get; set; }
        public double LandOwnerMountainShare { get; set; }
        public double LandOwnerAgricultureShare { get; set; }
        public double LandOwnerTotalShare { get; set; }
        public List<OwnerInfo> Owners { get; set; }

        public float TotalArea => ForestArea + MountainArea + AgricultureArea;

        public int LandsCount { get; set; }
    }
}

//public class OwnerInfo
//{
//    public OwnerInfo()
//    {
//        this.Owners = new List<OwnerInfo>();
//    }
//    public bool IsSharedLand { get; set; }
//    public int LandId { get; set; }
//    public string LandIdListStr { get; set; }
//    public int ContactOwnerLandId { get; set; }
//    public int LandOwnerId { get; set; }
//    public int SystemUserId { get; set; }
//    public string DisplayName { get; set; }
//    public string FName { get; set; }
//    public string LName { get; set; }
//    public string FullName { get; set; }
//    public string Email { get; set; }
//    public string ContactNumber { get; set; }
//    public string AddLine1 { get; set; }
//    public string AddLine2 { get; set; }
//    public string AddCity { get; set; }
//    public string BankAccountNo { get; set; }
//    public string Notes { get; set; }
//    public string LandNote { get; set; }
//    public string Unit { get; set; }
//    public int NoOfOccurrences { get; set; }
//    public float ForestArea { get; set; }
//    public float MountainArea { get; set; }
//    public float AgricultureArea { get; set; }
//    public double landOwnerForestShare { get; set; }
//    public double landOwnerMountainShare { get; set; }
//    public double landOwnerAgricultureShare { get; set; }
//    public double landOwnerTotalShare { get; set; }

//    public List<OwnerInfo> Owners { get; set; }
//    public float TotalArea
//    {
//        get { return this.ForestArea + this.MountainArea + this.AgricultureArea; }
//    }

//    public int LandsCount { get; set; }
//}

