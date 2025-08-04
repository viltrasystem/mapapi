using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class Quotum
{
    public int QuotaId { get; set; }

    public int UnitId { get; set; }

    public int AnimalTypeId { get; set; }

    public int GameId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AnimalType AnimalType { get; set; } = null!;

    public virtual HuntingGame Game { get; set; } = null!;

    public virtual ICollection<QuotaCount> QuotaCounts { get; set; } = new List<QuotaCount>();

    public virtual Unit Unit { get; set; } = null!;
}
