using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class ReportImage
{
    public int Id { get; set; }

    public byte[]? FileSaved { get; set; }

    public int ActionId { get; set; }

    public int TeamId { get; set; }

    public int DnnUserId { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public DateTime? FileCreated { get; set; }

    public string Name { get; set; } = null!;
}
