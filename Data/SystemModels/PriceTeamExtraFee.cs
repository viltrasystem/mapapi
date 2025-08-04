using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class PriceTeamExtraFee
{
    public int TeamExtraFeeId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public decimal ExtraFee { get; set; }

    public int ExtraFeeVatId { get; set; }

    public int PriceRegisterId { get; set; }

    public int TeamId { get; set; }

    public int CreatedDnnId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int HuntingYear { get; set; }

    public bool IsActive { get; set; }

    public int? ModifiedDnnId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual VatRegistration ExtraFeeVat { get; set; } = null!;

    public virtual PriceRegister PriceRegister { get; set; } = null!;
}
