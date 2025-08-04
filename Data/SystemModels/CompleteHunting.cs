using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class CompleteHunting
{
    public int Id { get; set; }

    public int UnitId { get; set; }

    public int HuntingYear { get; set; }

    public int UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public int? AnimalId { get; set; }

    public int? GameId { get; set; }

    public virtual Unit Unit { get; set; } = null!;
}
