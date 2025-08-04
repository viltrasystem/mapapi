using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class AnimalMappedToShotOb
{
    public int AnimalMappedToShotObsId { get; set; }

    public int ShotSubtypeId { get; set; }

    public int ObsSubtypeId { get; set; }

    public bool IsActive { get; set; }

    public virtual AnimalSubType ObsSubtype { get; set; } = null!;

    public virtual AnimalSubType ShotSubtype { get; set; } = null!;
}
