using ViltrapportenApi.Data.SystemModels;

namespace ViltrapportenApi.Modal
{
    public class LandOwnerInformation
    {
            public int LandOwnerId { get; set; }

            public int LandId { get; set; }

            public int SystemUserId { get; set; }

            public int AssignedBy { get; set; }

            public DateTime AssignedOn { get; set; }

            public bool IsActive { get; set; }

            public virtual Land Land { get; set; } = null!;
             public string OwnerName { get; set; } = null!;
             public string Email { get; set; } = null!;
              public string ContactNo { get; set; } = null!;
    }
}
