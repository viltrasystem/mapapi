using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class StatisticsExcelImportHuntingMethodDetail
{
    public int ExcelImpHuntingMethodDetailId { get; set; }

    public int HuntingMethodId { get; set; }

    public string KeyWordInExcelFile { get; set; } = null!;

    public int ExcelImpMasterId { get; set; }

    public virtual StatisticsExcelImportMaster ExcelImpMaster { get; set; } = null!;

    public virtual AnimalHasHuntingMethod HuntingMethod { get; set; } = null!;
}
