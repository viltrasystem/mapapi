using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace ViltrapportenApi.Data.MapModels;

/// <summary>
/// land drawn by edit users using existing layer
/// </summary>
public partial class DrawnLand
{
    public int DrawnLandId { get; set; }

    public Geometry Geometry { get; set; } = null!;

    public int CreatedBy { get; set; }

    public int? EditedBy { get; set; }

    public bool IsArchived { get; set; }

    public int TeigId { get; set; }

    public Guid UuidLandDrawn { get; set; }

    public int LandId { get; set; }
}
