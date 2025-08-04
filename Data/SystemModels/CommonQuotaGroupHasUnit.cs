using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class CommonQuotaGroupHasUnit
{
    public int GroupHasUnitId { get; set; }

    public int UnitId { get; set; }

    public int GroupId { get; set; }

    public virtual CommonQuotaGroup Group { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
