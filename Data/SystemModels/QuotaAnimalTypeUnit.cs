using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class QuotaAnimalTypeUnit
{
    public int Id { get; set; }

    public int UnitId { get; set; }

    public int QuotaAnimalTypeId { get; set; }

    public int? SharedByDnnId { get; set; }

    public virtual QuotaAnimalType QuotaAnimalType { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
