using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class PriceCompanyRegister
{
    public int CompanyRegisterId { get; set; }

    public int CreatedUnitId { get; set; }

    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public bool IsVatEnable { get; set; }

    public bool IsInvoiceEnable { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedDnnId { get; set; }

    public bool IsActive { get; set; }

    public int? EditedDnnId { get; set; }

    public DateTime? EditedDate { get; set; }

    public int? PriceDnnUserId { get; set; }

    public virtual ICollection<PriceAccountRegister> PriceAccountRegisters { get; set; } = new List<PriceAccountRegister>();

    public virtual ICollection<PriceRegister> PriceRegisters { get; set; } = new List<PriceRegister>();
}
