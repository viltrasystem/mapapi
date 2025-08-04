using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class QuotaCount
{
    public int QuotaCountId { get; set; }

    public int QuotaId { get; set; }

    public int QuotaAnimalTypeId { get; set; }

    public int NormalQuotaCount { get; set; }

    public int FreeQuotaCount { get; set; }

    public int? PriceExtraCount { get; set; }

    public int? ExtraQuotaCount { get; set; }

    public virtual Quotum Quota { get; set; } = null!;

    public virtual QuotaAnimalType QuotaAnimalType { get; set; } = null!;
}
