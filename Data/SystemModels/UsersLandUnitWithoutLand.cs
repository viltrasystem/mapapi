using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class UsersLandUnitWithoutLand
{
    public int UserLandUnitWithoutLandId { get; set; }

    public int UserSysId { get; set; }

    public int LandUnitId { get; set; }

    public bool IsActive { get; set; }

    public virtual Unit LandUnit { get; set; } = null!;

    public virtual ViltraUser UserSys { get; set; } = null!;
}
