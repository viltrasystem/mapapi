using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class PricePerQuotum
{
    public int PriceQuotaId { get; set; }

    public int QuotaCategoryId { get; set; }

    public decimal? PricePerQuotaValue { get; set; }

    public decimal? DescriptionFee { get; set; }

    public int PriceRegisterId { get; set; }

    public virtual PriceRegister PriceRegister { get; set; } = null!;
}
