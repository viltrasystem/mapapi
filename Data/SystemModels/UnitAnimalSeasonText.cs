using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class UnitAnimalSeasonText
{
    public int UnitAnimalSeasonId { get; set; }

    public int UnitId { get; set; }

    public int AnimalId { get; set; }

    public string SeasonText { get; set; } = null!;

    public int Season { get; set; }

    public bool IsActive { get; set; }

    public virtual AnimalType Animal { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
