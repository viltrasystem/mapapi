using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class QuotaTypesMappedToAntlersType
{
    public int QuotaTypesMappedToAntlersId { get; set; }

    public int QuotaTypeId { get; set; }

    public int AntlersTypeId { get; set; }

    public virtual AnimalHasAntler AntlersType { get; set; } = null!;

    public virtual QuotaAnimalType QuotaType { get; set; } = null!;
}
