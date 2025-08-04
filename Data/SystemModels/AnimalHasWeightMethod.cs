using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class AnimalHasWeightMethod
{
    public int WeightMethodId { get; set; }

    public string WeightMethod { get; set; } = null!;

    public int AnimalTypeId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDefault { get; set; }

    public string WeightMethodRef { get; set; } = null!;

    public virtual AnimalType AnimalType { get; set; } = null!;
}
