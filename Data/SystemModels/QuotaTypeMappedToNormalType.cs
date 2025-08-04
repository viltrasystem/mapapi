using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class QuotaTypeMappedToNormalType
{
    public int QuotaMappedTypeId { get; set; }

    public int QuotaTypeId { get; set; }

    public int NormalTypeId { get; set; }

    public virtual AnimalSubType NormalType { get; set; } = null!;

    public virtual QuotaAnimalType QuotaType { get; set; } = null!;
}
