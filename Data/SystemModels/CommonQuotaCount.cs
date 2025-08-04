using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class CommonQuotaCount
{
    public int CommonQuotaCountId { get; set; }

    public int CommonQuotaId { get; set; }

    public int QuotaTypeId { get; set; }

    public int CommonQuotaCount1 { get; set; }

    public virtual CommonQuotum CommonQuota { get; set; } = null!;

    public virtual QuotaAnimalType QuotaType { get; set; } = null!;
}
