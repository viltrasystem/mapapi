using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class UnitGroupUnit
{
    public int UnitGroupUnitId { get; set; }

    public int UnitGroupId { get; set; }

    public int UnitId { get; set; }

    public bool IsActive { get; set; }

    public int? MappedRegisteredUnitId { get; set; }

    public virtual Unit Unit { get; set; } = null!;

    public virtual UnitGroup UnitGroup { get; set; } = null!;
}
