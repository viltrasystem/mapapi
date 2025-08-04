using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class LandOwner
{
    public int LandOwnerId { get; set; }

    public int LandId { get; set; }

    public int SystemUserId { get; set; }

    public int AssignedBy { get; set; }

    public DateTime AssignedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual Land Land { get; set; } = null!;

    public virtual ViltraUser SystemUser { get; set; } = null!;
}
