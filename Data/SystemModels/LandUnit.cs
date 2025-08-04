using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class LandUnit
{
    public int LandUnitId { get; set; }

    public int LandId { get; set; }

    public int UnitId { get; set; }

    public int LandTypeId { get; set; }

    public virtual Land Land { get; set; } = null!;

    public virtual LandType LandType { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
