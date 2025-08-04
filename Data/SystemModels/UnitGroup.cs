using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class UnitGroup
{
    public int UnitGroupId { get; set; }

    public string UnitGroupName { get; set; } = null!;

    public int? AdministrationUnitId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public bool IsPublic { get; set; }

    public virtual Unit? AdministrationUnit { get; set; }

    public virtual ICollection<UnitGroupUnit> UnitGroupUnits { get; set; } = new List<UnitGroupUnit>();
}
