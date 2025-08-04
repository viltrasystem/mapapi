using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class DistributionFilterUnit
{
    public int DistributionUnitId { get; set; }

    public int DistributionId { get; set; }

    public int UnitId { get; set; }

    public bool IsActive { get; set; }

    public virtual DistributionList Distribution { get; set; } = null!;
}
