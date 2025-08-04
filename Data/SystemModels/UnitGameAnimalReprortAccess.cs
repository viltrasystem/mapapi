using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class UnitGameAnimalReprortAccess
{
    public int UnitGameAnimalRepAccessId { get; set; }

    public int UnitId { get; set; }

    public int GameId { get; set; }

    public int AnimalId { get; set; }

    public bool IsReportingActive { get; set; }

    public int Season { get; set; }

    public virtual AnimalType Animal { get; set; } = null!;

    public virtual HuntingGame Game { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
