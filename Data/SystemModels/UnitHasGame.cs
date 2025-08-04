using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class UnitHasGame
{
    public int UnitHasGameId { get; set; }

    public int UnitId { get; set; }

    public int GameId { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual HuntingGame Game { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
