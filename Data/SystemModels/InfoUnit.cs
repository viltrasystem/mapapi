using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class InfoUnit
{
    public int InfoUnitId { get; set; }

    public int InfoId { get; set; }

    public int UnitId { get; set; }

    public bool IsActive { get; set; }

    public virtual UnitsInfomation Info { get; set; } = null!;
}
