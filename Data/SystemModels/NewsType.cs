using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class NewsType
{
    public int NewsTypeId { get; set; }

    public string NewsType1 { get; set; } = null!;

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
