using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class AnimalHasAntler
{
    public int AntlersId { get; set; }

    public string AntlersType { get; set; } = null!;

    public int AnimalTypeId { get; set; }

    public bool IsActive { get; set; }

    public string? SmsCode { get; set; }

    public virtual AnimalType AnimalType { get; set; } = null!;

    public virtual ICollection<QuotaTypesMappedToAntlersType> QuotaTypesMappedToAntlersTypes { get; set; } = new List<QuotaTypesMappedToAntlersType>();
}
