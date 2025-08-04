using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class LandownershipType
{
    public int OwnershipTypeId { get; set; }

    public string OwnershipType { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Land> Lands { get; set; } = new List<Land>();
}
