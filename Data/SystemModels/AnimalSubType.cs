using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class AnimalSubType
{
    public int AnimalSubTypeId { get; set; }

    public int AnimalTypeId { get; set; }

    public string AnimalSubType1 { get; set; } = null!;

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsGroup { get; set; }

    public int NoOfAnimals { get; set; }

    public int? NoOfElders { get; set; }

    public int? NoOfChild { get; set; }

    public int? Category { get; set; }

    public int? NoOfElderMales { get; set; }

    public int? NoOfElderFemales { get; set; }

    public int? NoOfChildMales { get; set; }

    public int? NoOfChildFemales { get; set; }

    public int? NoOfUnknownAnimals { get; set; }

    public int? NoOfElderGenderUnknown { get; set; }

    public int? NoOfChildGenderUnknown { get; set; }

    public string? Smscode { get; set; }

    public bool AntlersRequired { get; set; }

    public bool IsTagsRequired { get; set; }

    public bool IsAntlersExistsForSubType { get; set; }

    public float MinWeight { get; set; }

    public float MaxWeight { get; set; }

    public int? Smssequence { get; set; }

    public int? OrderValue { get; set; }

    public float? SubTypeAge { get; set; }

    public virtual ICollection<AnimalActionAnimalDetail> AnimalActionAnimalDetails { get; set; } = new List<AnimalActionAnimalDetail>();

    public virtual ICollection<AnimalActionDetail> AnimalActionDetails { get; set; } = new List<AnimalActionDetail>();

    public virtual ICollection<AnimalDetailsWeightChangedLog> AnimalDetailsWeightChangedLogs { get; set; } = new List<AnimalDetailsWeightChangedLog>();

    public virtual ICollection<AnimalMappedToShotOb> AnimalMappedToShotObObsSubtypes { get; set; } = new List<AnimalMappedToShotOb>();

    public virtual ICollection<AnimalMappedToShotOb> AnimalMappedToShotObShotSubtypes { get; set; } = new List<AnimalMappedToShotOb>();

    public virtual AnimalType AnimalType { get; set; } = null!;

    public virtual ICollection<QuotaTypeMappedToNormalType> QuotaTypeMappedToNormalTypes { get; set; } = new List<QuotaTypeMappedToNormalType>();

    public virtual ICollection<StatisticsExcelImportSubTypeDetail> StatisticsExcelImportSubTypeDetails { get; set; } = new List<StatisticsExcelImportSubTypeDetail>();
}
