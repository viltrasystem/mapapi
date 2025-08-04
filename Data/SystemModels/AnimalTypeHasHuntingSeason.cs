using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class AnimalTypeHasHuntingSeason
{
    public int SeasonId { get; set; }

    public int AnimalTypeId { get; set; }

    public string StartDate { get; set; } = null!;

    public string EndDate { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual AnimalType AnimalType { get; set; } = null!;
}
