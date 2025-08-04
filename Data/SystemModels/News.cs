using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class News
{
    public int NewsId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime NewsDate { get; set; }

    public string Summary { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public int Sequence { get; set; }

    public int NewsTypeId { get; set; }

    public virtual NewsType NewsType { get; set; } = null!;
}
