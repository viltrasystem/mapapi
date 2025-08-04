namespace ViltrapportenApi.Modal
{
    public class LandOwnerRegisterDetail
    {
        public int? LandId { get; set; }
        public int SystemUserId { get; set; }
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
        public List<OwnersState> OwnersStates { get; set; }
    }
}
