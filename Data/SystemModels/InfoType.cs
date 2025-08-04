using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class InfoType
{
    public int InfoTypeId { get; set; }

    public string Info { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<UnitsInfomation> UnitsInfomations { get; set; } = new List<UnitsInfomation>();
}
