using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class VatRegistration
{
    public int VatRegisterId { get; set; }

    public string Season { get; set; } = null!;

    public double Percentage { get; set; }

    public string ShortCode { get; set; } = null!;

    public string? Name { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<PriceExtraFee> PriceExtraFees { get; set; } = new List<PriceExtraFee>();

    public virtual ICollection<PriceTeamExtraFee> PriceTeamExtraFees { get; set; } = new List<PriceTeamExtraFee>();
}
