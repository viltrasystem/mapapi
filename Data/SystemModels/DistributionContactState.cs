using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class DistributionContactState
{
    public int DistributionContactStateId { get; set; }

    public string KeyWord { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public bool IsEnable { get; set; }
}
