using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class InfoUser
{
    public int InfoUserId { get; set; }

    public int InfoId { get; set; }

    public int UserId { get; set; }

    public int UnitId { get; set; }

    public bool IsActive { get; set; }

    public virtual UnitsInfomation Info { get; set; } = null!;
}
