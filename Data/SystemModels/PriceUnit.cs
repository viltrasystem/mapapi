using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class PriceUnit
{
    public int PriceUnitId { get; set; }

    public int TeamId { get; set; }

    public bool IsActive { get; set; }

    public int PriceRegisterId { get; set; }

    public bool IsPublish { get; set; }

    public DateTime? FinalMailSentDate { get; set; }

    public int? AdvanceSenderDnnId { get; set; }

    public int? FinalSenderDnnId { get; set; }

    public bool IsFinalMailSent { get; set; }

    public DateTime? AdvanceMailSentDate { get; set; }

    public decimal? SumTurnOver { get; set; }

    public decimal? AdvTotal { get; set; }

    public virtual PriceRegister PriceRegister { get; set; } = null!;
}
