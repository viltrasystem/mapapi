using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class DistributionList
{
    public int ListId { get; set; }

    public string ListName { get; set; } = null!;

    public int GameId { get; set; }

    public int AnimalTypeId { get; set; }

    public bool IsActive { get; set; }

    public int UnitId { get; set; }

    public string Smscategory { get; set; } = null!;

    public bool IsEnable { get; set; }

    public virtual AnimalType AnimalType { get; set; } = null!;

    public virtual ICollection<DistributionContactList> DistributionContactLists { get; set; } = new List<DistributionContactList>();

    public virtual ICollection<DistributionFilterList> DistributionFilterLists { get; set; } = new List<DistributionFilterList>();

    public virtual ICollection<DistributionFilterUnit> DistributionFilterUnits { get; set; } = new List<DistributionFilterUnit>();

    public virtual HuntingGame Game { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
