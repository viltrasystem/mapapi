using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class PriceWeightRange
{
    public int PriceWeightRangeId { get; set; }

    public int ShotCategoryId { get; set; }

    public int WeightStart { get; set; }

    public int WeightEnd { get; set; }

    public decimal PricePerKilo { get; set; }

    public int PriceRegisterId { get; set; }

    public bool IsActive { get; set; }

    public virtual PriceRegister PriceRegister { get; set; } = null!;
}
