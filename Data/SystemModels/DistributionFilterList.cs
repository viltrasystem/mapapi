using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class DistributionFilterList
{
    public int DistributionFilterListId { get; set; }

    public int DistributionListId { get; set; }

    public bool IsAllLandOwners { get; set; }

    public bool IsHeadHunters { get; set; }

    public bool IsReportingHunters { get; set; }

    public bool IsTeamLeaders { get; set; }

    public bool IsGeneralHunters { get; set; }

    public bool IsTeamsInCommonQuota { get; set; }

    public bool IsHeadUpperUsers { get; set; }

    public bool IsReportingUpperUsers { get; set; }

    public bool IsMunicipalityUpperUsers { get; set; }

    public bool IsGuestUpperUsers { get; set; }

    public bool IsGeneralUpperUsers { get; set; }

    public bool IsAllInactiveUsers { get; set; }

    public bool IsAllChildren { get; set; }

    public bool IsActive { get; set; }

    public bool IsExporterUpperUsers { get; set; }

    public virtual DistributionList DistributionList { get; set; } = null!;
}
