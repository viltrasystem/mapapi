namespace ViltrapportenApi.Modal
{
    public class LandOwnerInfo
    {
        public int LandOwnerId { get; set; }
        public int SystemUserId { get; set; }
        public int DnnUserId { get; set; }
        public int LandId { get; set; }
        public int ContactOwnerLandId { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public bool IsSelected { get; set; }
        public bool IsSharedLandOwner { get; set; }
        public bool IsAdmin { get; set; }
    }
}
