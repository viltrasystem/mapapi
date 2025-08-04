using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class AnimalDetailsWeightChangedLog
{
    public int AnimalDetailsWeightChangedLogId { get; set; }

    public int? AnimalDetailsId { get; set; }

    public int ActionId { get; set; }

    public float? AnimalWeight { get; set; }

    public int? WeightIsEstimated { get; set; }

    public DateTime? DateWeighted { get; set; }

    public int? AnimalSubTypeId { get; set; }

    public virtual AnimalAction Action { get; set; } = null!;

    public virtual AnimalActionAnimalDetail? AnimalDetails { get; set; }

    public virtual AnimalSubType? AnimalSubType { get; set; }
}
