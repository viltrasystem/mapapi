using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class UnitType
{
    public int UnitTypeId { get; set; }

    public string UnitType1 { get; set; } = null!;

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime DateCreated { get; set; }

    public int? DnnRoleId { get; set; }

    public string? ImgUrl { get; set; }

    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
}
