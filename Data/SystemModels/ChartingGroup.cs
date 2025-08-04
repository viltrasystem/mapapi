using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class ChartingGroup
{
    public int Id { get; set; }

    public string GroupName { get; set; } = null!;

    public bool IsActive { get; set; }

    public int TypeId { get; set; }

    public string Value { get; set; } = null!;

    public int AnimalId { get; set; }
}
