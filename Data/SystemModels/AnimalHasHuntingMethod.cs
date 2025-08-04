using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class AnimalHasHuntingMethod
{
    public int MethodId { get; set; }

    public string MethodType { get; set; } = null!;

    public string MethodRef { get; set; } = null!;

    public int AnimalTypeId { get; set; }

    public bool IsActive { get; set; }

    public virtual AnimalType AnimalType { get; set; } = null!;

    public virtual ICollection<StatisticsExcelImportHuntingMethodDetail> StatisticsExcelImportHuntingMethodDetails { get; set; } = new List<StatisticsExcelImportHuntingMethodDetail>();
}
