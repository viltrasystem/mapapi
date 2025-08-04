using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class AnimalType
{
    public int AnimalTypeId { get; set; }

    public string AnimalType1 { get; set; } = null!;

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsPredator { get; set; }

    public float? MaxNoOfHours { get; set; }

    public bool? IsAntlersExists { get; set; }

    public string? SmsCodes { get; set; }

    public bool IsHuntingDogsAllow { get; set; }

    public int IsBigAnimal { get; set; }

    public int? NoOfHunters { get; set; }

    public float? NoOfHours { get; set; }

    public bool IsNoOfHuntersEditable { get; set; }

    public bool IsNoOfHoursEditable { get; set; }

    public bool IsPlaceMandatory { get; set; }

    public bool IsExport { get; set; }

    public bool IsShotObsMapped { get; set; }

    public bool IsCheckUnlogic { get; set; }

    public bool IsWeightMethodsEnabled { get; set; }

    public bool IsUnlogicNotify { get; set; }

    public bool IsColdWarmWeightEnabled { get; set; }

    public int? MaxColdWeightDays { get; set; }

    public bool IsShotAloneExportable { get; set; }

    public virtual ICollection<AnimalAction> AnimalActions { get; set; } = new List<AnimalAction>();

    public virtual ICollection<AnimalHasAntler> AnimalHasAntlers { get; set; } = new List<AnimalHasAntler>();

    public virtual ICollection<AnimalHasHuntingMethod> AnimalHasHuntingMethods { get; set; } = new List<AnimalHasHuntingMethod>();

    public virtual ICollection<AnimalHasWeightMethod> AnimalHasWeightMethods { get; set; } = new List<AnimalHasWeightMethod>();

    public virtual ICollection<AnimalSubType> AnimalSubTypes { get; set; } = new List<AnimalSubType>();

    public virtual ICollection<AnimalTypeHasHuntingSeason> AnimalTypeHasHuntingSeasons { get; set; } = new List<AnimalTypeHasHuntingSeason>();

    public virtual ICollection<CommonQuotum> CommonQuota { get; set; } = new List<CommonQuotum>();

    public virtual ICollection<DistributionList> DistributionLists { get; set; } = new List<DistributionList>();

    public virtual ICollection<GameHasAnimal> GameHasAnimals { get; set; } = new List<GameHasAnimal>();

    public virtual ICollection<Quotum> Quota { get; set; } = new List<Quotum>();

    public virtual ICollection<QuotaAnimalType> QuotaAnimalTypes { get; set; } = new List<QuotaAnimalType>();

    public virtual ICollection<ReportExport> ReportExports { get; set; } = new List<ReportExport>();

    public virtual ICollection<StatisticsExcelImportMaster> StatisticsExcelImportMasters { get; set; } = new List<StatisticsExcelImportMaster>();

    public virtual ICollection<TeamHasDogRegisterAnimal> TeamHasDogRegisterAnimals { get; set; } = new List<TeamHasDogRegisterAnimal>();

    public virtual ICollection<UnitAnimalSeasonText> UnitAnimalSeasonTexts { get; set; } = new List<UnitAnimalSeasonText>();

    public virtual ICollection<UnitGameAnimalReprortAccess> UnitGameAnimalReprortAccesses { get; set; } = new List<UnitGameAnimalReprortAccess>();
}
