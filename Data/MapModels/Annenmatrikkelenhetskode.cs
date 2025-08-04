using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.MapModels;

public partial class Annenmatrikkelenhetskode
{
    public string Identifier { get; set; } = null!;

    public string? Description { get; set; }
}
