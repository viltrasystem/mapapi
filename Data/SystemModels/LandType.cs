using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class LandType
{
    public int LandTypeId { get; set; }

    public string LandTypeName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<LandUnit> LandUnits { get; set; } = new List<LandUnit>();
}
