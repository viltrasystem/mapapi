using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class CommonQuotum
{
    public int CommonQuotaId { get; set; }

    public int GroupId { get; set; }

    public int AnimalTypeId { get; set; }

    public int GameId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; }

    public virtual AnimalType AnimalType { get; set; } = null!;

    public virtual ICollection<CommonQuotaCount> CommonQuotaCounts { get; set; } = new List<CommonQuotaCount>();

    public virtual HuntingGame Game { get; set; } = null!;

    public virtual CommonQuotaGroup Group { get; set; } = null!;
}
