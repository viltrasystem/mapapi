using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class StatisticsExcelImportMaster
{
    public int ExcelImpMasterId { get; set; }

    public int AnimalTypeId { get; set; }

    public int HeaderRowId { get; set; }

    public int DataStartingRowId { get; set; }

    public string DateColumnId { get; set; } = null!;

    public string? HuntersCountColumnId { get; set; }

    public string? HoursCountColumnId { get; set; }

    public string? IsActionAtForestColumnId { get; set; }

    public string? HuntingMethodColumnId { get; set; }

    public string? WeightColumnId { get; set; }

    public int TemplateTypeId { get; set; }

    public string? SampleFileName { get; set; }

    public virtual AnimalType AnimalType { get; set; } = null!;

    public virtual ICollection<StatisticsExcelImportHuntingMethodDetail> StatisticsExcelImportHuntingMethodDetails { get; set; } = new List<StatisticsExcelImportHuntingMethodDetail>();

    public virtual ICollection<StatisticsExcelImportSubTypeDetail> StatisticsExcelImportSubTypeDetails { get; set; } = new List<StatisticsExcelImportSubTypeDetail>();
}
