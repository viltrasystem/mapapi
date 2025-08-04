using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class AnimalAction
{
    public int ActionId { get; set; }

    public bool IsShot { get; set; }

    public int AnimalTypeId { get; set; }

    public int? HunterId { get; set; }

    public int HuntingTeamId { get; set; }

    public int NoOfHuntersWithGun { get; set; }

    public float NoOfHoursSpent { get; set; }

    public DateTimeOffset DateCreated { get; set; }

    public DateTime ActionDate { get; set; }

    public string? Comments { get; set; }

    public bool IsActive { get; set; }

    public int GameId { get; set; }

    public int ReporterDnnId { get; set; }

    public int? LastUpdatedBy { get; set; }

    public bool? IsActionAtForest { get; set; }

    public int? OldId { get; set; }

    public bool IsSms { get; set; }

    public int IsCommentImportant { get; set; }

    public string? ActionTime { get; set; }

    public string? ReportedNumber { get; set; }

    public string? HunterNameOptional { get; set; }

    public bool IsExcelImport { get; set; }

    public bool? IsObsMapped { get; set; }

    public string? ObsNonMappedComment { get; set; }

    public virtual ICollection<AnimalActionAnimalDetail> AnimalActionAnimalDetails { get; set; } = new List<AnimalActionAnimalDetail>();

    public virtual ICollection<AnimalActionDetail> AnimalActionDetails { get; set; } = new List<AnimalActionDetail>();

    public virtual ICollection<AnimalDetailsWeightChangedLog> AnimalDetailsWeightChangedLogs { get; set; } = new List<AnimalDetailsWeightChangedLog>();

    public virtual AnimalType AnimalType { get; set; } = null!;

    public virtual HuntingGame Game { get; set; } = null!;

    public virtual Unit HuntingTeam { get; set; } = null!;

    public virtual ICollection<ReportExport> ReportExports { get; set; } = new List<ReportExport>();
}
