using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace ViltrapportenApi.Data.MapModels;

public partial class FeatureDrawn
{
    public int Id { get; set; }

    public Geometry Geometry { get; set; } = null!;

    public string Properties { get; set; } = null!;

    public string? Style { get; set; }

    public Guid UuidFeatureDrawn { get; set; }

    public string? IconType { get; set; }
}
