using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class AnimalActionDetail
{
    public int AnimalActionDetailId { get; set; }

    public int AnimalActionId { get; set; }

    public int AnimalSubTypeId { get; set; }

    public int ActionCount { get; set; }

    public int QuotaSubTypeId { get; set; }

    public bool IsFromQuotaTypeTable { get; set; }

    public virtual AnimalAction AnimalAction { get; set; } = null!;

    public virtual AnimalSubType AnimalSubType { get; set; } = null!;
}
