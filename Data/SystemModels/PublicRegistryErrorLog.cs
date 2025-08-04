using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class PublicRegistryErrorLog
{
    public int ErrorId { get; set; }

    public string? ErrorThrown { get; set; }

    public string TextStatus { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int UserDnnId { get; set; }

    public DateTimeOffset ErrorDateTime { get; set; }

    public bool IsShot { get; set; }

    public int GameId { get; set; }

    public int AnimalId { get; set; }

    public string? ReferenceId { get; set; }

    public int TeamId { get; set; }

    public int UnitId { get; set; }

    public string? ManagementReferenceId { get; set; }

    public int? ManagementUnitId { get; set; }
}
