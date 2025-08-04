using static ViltrapportenApi.Modal.ApiRoutes;

namespace ViltrapportenApi.Modal
{
    public class OwnerDetail // Land owner form details
    {

        public int? LandId { get; set; }
        public int SystemUserId { get; set; }
        public string? BankAccountNo { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressCity { get; set; }
        public string? Notes { get; set; }

    }
}
