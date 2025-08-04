using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace ViltrapportenApi.Data.MapModels;

/// <summary>
/// land drawn by edit users using existing layer
/// </summary>
public partial class LandDrawn
{
    public int LandDrawnId { get; set; }

    public Geometry Geometry { get; set; } = null!;

    public int CreatedBy { get; set; }

    public int? EditedBy { get; set; }

    public bool IsArchived { get; set; }

    public long TeigId { get; set; }

    public Guid UuidLandDrawn { get; set; }

    public int LandId { get; set; }

    public string? Properties { get; set; }

    public string MunicipalityName { get; set; } = null!;

    public string MunicipalityNo { get; set; } = null!;

    public string MainNo { get; set; } = null!;

    public string SubNo { get; set; } = null!;

    public string? PlotNo { get; set; }
}
