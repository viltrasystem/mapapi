using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class WeightType
{
    public int WeightTypeId { get; set; }

    public string WeightType1 { get; set; } = null!;

    public bool IsActive { get; set; }
}
