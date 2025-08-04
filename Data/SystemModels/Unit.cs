using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class Unit
{
    public int UnitId { get; set; }

    public string Unit1 { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime DateCreated { get; set; }

    public string? ReferenceId { get; set; }

    public bool IsActive { get; set; }

    public string? Area { get; set; }

    public bool IsArchived { get; set; }

    public bool IsAllowedToRegisterLands { get; set; }

    public int TypeId { get; set; }

    public int? ParentId { get; set; }

    public virtual ICollection<AnimalAction> AnimalActions { get; set; } = new List<AnimalAction>();

    public virtual ICollection<CommonQuotaGroupHasUnit> CommonQuotaGroupHasUnits { get; set; } = new List<CommonQuotaGroupHasUnit>();

    public virtual ICollection<CommonQuotaGroup> CommonQuotaGroups { get; set; } = new List<CommonQuotaGroup>();

    public virtual ICollection<CompleteHunting> CompleteHuntings { get; set; } = new List<CompleteHunting>();

    public virtual ICollection<DistributionList> DistributionLists { get; set; } = new List<DistributionList>();

    public virtual ICollection<HuntersAvailableForSeason> HuntersAvailableForSeasons { get; set; } = new List<HuntersAvailableForSeason>();

    public virtual ICollection<HuntingDocSigned> HuntingDocSigneds { get; set; } = new List<HuntingDocSigned>();

    public virtual ICollection<HuntingTeamStayInfo> HuntingTeamStayInfos { get; set; } = new List<HuntingTeamStayInfo>();

    public virtual ICollection<Unit> InverseParent { get; set; } = new List<Unit>();

    public virtual ICollection<LandUnit> LandUnits { get; set; } = new List<LandUnit>();

    public virtual Unit? Parent { get; set; }

    public virtual ICollection<PriceRegister> PriceRegisters { get; set; } = new List<PriceRegister>();

    public virtual ICollection<Quotum> Quota { get; set; } = new List<Quotum>();

    public virtual ICollection<QuotaAnimalTypeUnit> QuotaAnimalTypeUnits { get; set; } = new List<QuotaAnimalTypeUnit>();

    public virtual ICollection<QuotaAnimalType> QuotaAnimalTypes { get; set; } = new List<QuotaAnimalType>();

    public virtual UnitType Type { get; set; } = null!;

    public virtual ICollection<UnitAnimalSeasonText> UnitAnimalSeasonTexts { get; set; } = new List<UnitAnimalSeasonText>();

    public virtual ICollection<UnitAvailabilityForSeason> UnitAvailabilityForSeasons { get; set; } = new List<UnitAvailabilityForSeason>();

    public virtual ICollection<UnitGameAnimalReprortAccess> UnitGameAnimalReprortAccesses { get; set; } = new List<UnitGameAnimalReprortAccess>();

    public virtual ICollection<UnitGroupUnit> UnitGroupUnits { get; set; } = new List<UnitGroupUnit>();

    public virtual ICollection<UnitGroup> UnitGroups { get; set; } = new List<UnitGroup>();

    public virtual ICollection<UnitHasGame> UnitHasGames { get; set; } = new List<UnitHasGame>();

    public virtual ICollection<UnitHasUser> UnitHasUsers { get; set; } = new List<UnitHasUser>();

    public virtual ICollection<UnitsInfomation> UnitsInfomations { get; set; } = new List<UnitsInfomation>();

    public virtual ICollection<UsersLandUnitWithoutLand> UsersLandUnitWithoutLands { get; set; } = new List<UsersLandUnitWithoutLand>();
}
