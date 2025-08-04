using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class PriceExtraFee
{
    public int PriceExtraFeeId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public decimal? ExtraFee { get; set; }

    public int ExtraFeeVatId { get; set; }

    public int PriceRegisterId { get; set; }

    public virtual VatRegistration ExtraFeeVat { get; set; } = null!;

    public virtual PriceRegister PriceRegister { get; set; } = null!;
}
