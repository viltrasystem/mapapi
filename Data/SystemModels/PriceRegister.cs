using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class PriceRegister
{
    public int PriceRegisterId { get; set; }

    public int UnitId { get; set; }

    public string PriceGroupName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public string? Email { get; set; }

    public int Season { get; set; }

    public int CreatedDnnId { get; set; }

    public int? EditedDnnId { get; set; }

    public string? ShotDescriptionTitle { get; set; }

    public string? QuotaDescriptionTitle { get; set; }

    public int GenaralVatId { get; set; }

    public int QuotaDescVatId { get; set; }

    public int ShotDescVatId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? EditedDate { get; set; }

    public int GameId { get; set; }

    public int AnimalId { get; set; }

    public bool AdvAfterHunting { get; set; }

    public bool AdvBeforeHunting { get; set; }

    public int DueDays { get; set; }

    public bool ExFeeAsAdvance { get; set; }

    public bool ExFeeAsFinal { get; set; }

    public int CompanyRegisterId { get; set; }

    public int? AccountRegisterId { get; set; }

    public bool IsActive { get; set; }

    public virtual PriceAccountRegister? AccountRegister { get; set; }

    public virtual PriceCompanyRegister CompanyRegister { get; set; } = null!;

    public virtual ICollection<PriceExtraFee> PriceExtraFees { get; set; } = new List<PriceExtraFee>();

    public virtual ICollection<PricePayment> PricePayments { get; set; } = new List<PricePayment>();

    public virtual ICollection<PricePerQuotum> PricePerQuota { get; set; } = new List<PricePerQuotum>();

    public virtual ICollection<PricePerShotKilo> PricePerShotKilos { get; set; } = new List<PricePerShotKilo>();

    public virtual ICollection<PriceTeamExtraFee> PriceTeamExtraFees { get; set; } = new List<PriceTeamExtraFee>();

    public virtual ICollection<PriceUnit> PriceUnits { get; set; } = new List<PriceUnit>();

    public virtual ICollection<PriceWeightRange> PriceWeightRanges { get; set; } = new List<PriceWeightRange>();

    public virtual Unit Unit { get; set; } = null!;
}
