using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class CommonQuotaGroup
{
    public int GroupId { get; set; }

    public int OwnerUnitId { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string GroupName { get; set; } = null!;

    public int HuntingYear { get; set; }

    public int GameId { get; set; }

    public int AnimalId { get; set; }

    public virtual ICollection<CommonQuotum> CommonQuota { get; set; } = new List<CommonQuotum>();

    public virtual ICollection<CommonQuotaGroupHasUnit> CommonQuotaGroupHasUnits { get; set; } = new List<CommonQuotaGroupHasUnit>();

    public virtual Unit OwnerUnit { get; set; } = null!;
}
