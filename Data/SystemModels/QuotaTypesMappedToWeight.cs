using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class QuotaTypesMappedToWeight
{
    public int QuotaTypesMappedToWeightId { get; set; }

    public int QuotaTypeId { get; set; }

    public int WeightFrom { get; set; }

    public int WeightTo { get; set; }

    public virtual QuotaAnimalType QuotaType { get; set; } = null!;
}
