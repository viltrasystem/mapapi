using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class UnitAnimalsWeightType
{
    public int UnitWeightTypeId { get; set; }

    public int ColdMaxDays { get; set; }

    public int UnitId { get; set; }

    public int? EditedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? EditedDate { get; set; }

    public int WeightTypeId { get; set; }
}
