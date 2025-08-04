using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class PriceAccountRegister
{
    public int AccountRegisterId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public int CompanyRegisterId { get; set; }

    public bool IsActive { get; set; }

    public virtual PriceCompanyRegister CompanyRegister { get; set; } = null!;

    public virtual ICollection<PricePayment> PricePayments { get; set; } = new List<PricePayment>();

    public virtual ICollection<PriceRegister> PriceRegisters { get; set; } = new List<PriceRegister>();
}
