using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.MapModels;

public partial class Malemetode
{
    public string Identifier { get; set; } = null!;

    public string? Description { get; set; }
}
