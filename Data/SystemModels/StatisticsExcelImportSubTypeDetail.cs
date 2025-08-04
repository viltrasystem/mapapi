using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class StatisticsExcelImportSubTypeDetail
{
    public int ExcelImpSubTypeDetailId { get; set; }

    public int AnimalSubTypeId { get; set; }

    public string ColumnId { get; set; } = null!;

    public int ExcelImpMasterId { get; set; }

    public virtual AnimalSubType AnimalSubType { get; set; } = null!;

    public virtual StatisticsExcelImportMaster ExcelImpMaster { get; set; } = null!;
}
