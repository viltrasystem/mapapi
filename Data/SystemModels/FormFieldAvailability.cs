using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class FormFieldAvailability
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public bool IsShotForm { get; set; }

    public int FieldId { get; set; }

    public bool IsAvailable { get; set; }

    public bool IsActive { get; set; }

    public virtual HuntingGame Game { get; set; } = null!;
}
