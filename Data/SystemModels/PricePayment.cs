using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class PricePayment
{
    public int PaymentId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PayedDate { get; set; }

    public string? Comment { get; set; }

    public int TeamId { get; set; }

    public int CreatedDnnId { get; set; }

    public int PriceRegisterId { get; set; }

    public int Season { get; set; }

    public DateTime RegisterDate { get; set; }

    public int? ModifiedDnnId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public int AccountRegisterId { get; set; }

    public virtual PriceAccountRegister AccountRegister { get; set; } = null!;

    public virtual PriceRegister PriceRegister { get; set; } = null!;
}
