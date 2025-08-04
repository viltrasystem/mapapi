using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class QuotaTypesMappedToTag
{
    public int QuotaTypesMappedToTagsId { get; set; }

    public int QuotaTypeId { get; set; }

    public int TagsFrom { get; set; }

    public int TagsTo { get; set; }

    public virtual QuotaAnimalType QuotaType { get; set; } = null!;
}
