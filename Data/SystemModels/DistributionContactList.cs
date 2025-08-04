using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class DistributionContactList
{
    public int DistributionContactListId { get; set; }

    public int DistributionListId { get; set; }

    public string ContactNumber { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? ContactName { get; set; }

    public int UserRef { get; set; }

    public virtual DistributionList DistributionList { get; set; } = null!;
}
