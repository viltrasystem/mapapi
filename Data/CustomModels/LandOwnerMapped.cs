namespace ViltrapportenApi.Data.CustomModels
{
    public class LandOwnerMapped
    {
        public int LandOwnerId { get; set; }
        public int SystemUserId { get; set; }
        public int DnnUserId { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressCity { get; set; }
        public string BankAccountNo { get; set; }
        public string Notes { get; set; }
        public int LandId { get; set; }
        public string Municipality { get; set; }
        public string MainNo { get; set; }
        public string SubNo { get; set; }
        public int ContactOwnerLandId { get; set; }
        public float AreaInForest { get; set; }
        public float AreaInMountain { get; set; }
        public float AreaInAgriculture { get; set; }
        public string LandNotes { get; set; }
        public string Unit { get; set; }
        public string ParentUnit { get; set; }
    }
}
