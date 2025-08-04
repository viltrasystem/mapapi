using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class PricePerShotKilo
{
    public int PricePerShotId { get; set; }

    public int ShotCategoryId { get; set; }

    public bool IsWeight { get; set; }

    public decimal? PricePerShot { get; set; }

    public decimal? DescriptionFee { get; set; }

    public int PriceRegisterId { get; set; }

    public virtual PriceRegister PriceRegister { get; set; } = null!;
}
