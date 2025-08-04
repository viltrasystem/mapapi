using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class HuntingGame
{
    public int GameId { get; set; }

    public string GameName { get; set; } = null!;

    public string GameRef { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string GameInfo { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? DeActivatedOn { get; set; }

    public DateTime? LastUpdate { get; set; }

    public int? UpdatedBy { get; set; }

    public int ShotFormType { get; set; }

    public bool IsAllowedOnHomeSummary { get; set; }

    public string? SmsCodes { get; set; }

    public string? ShotSmsCode { get; set; }

    public string? ObsSmsCode { get; set; }

    public bool IsAvailableOnSmsdistribution { get; set; }

    public bool IsCostsAdministration { get; set; }

    public bool IsAvailableOnSeasonDoList { get; set; }

    public virtual ICollection<AnimalAction> AnimalActions { get; set; } = new List<AnimalAction>();

    public virtual ICollection<CommonQuotum> CommonQuota { get; set; } = new List<CommonQuotum>();

    public virtual ICollection<DistributionList> DistributionLists { get; set; } = new List<DistributionList>();

    public virtual ICollection<FormFieldAvailability> FormFieldAvailabilities { get; set; } = new List<FormFieldAvailability>();

    public virtual ICollection<GameHasAnimal> GameHasAnimals { get; set; } = new List<GameHasAnimal>();

    public virtual ICollection<Quotum> Quota { get; set; } = new List<Quotum>();

    public virtual ICollection<QuotaAnimalType> QuotaAnimalTypes { get; set; } = new List<QuotaAnimalType>();

    public virtual ICollection<UnitGameAnimalReprortAccess> UnitGameAnimalReprortAccesses { get; set; } = new List<UnitGameAnimalReprortAccess>();

    public virtual ICollection<UnitHasGame> UnitHasGames { get; set; } = new List<UnitHasGame>();
}
