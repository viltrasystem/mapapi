using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class ReportExport
{
    public int ExportId { get; set; }

    public int? ActionId { get; set; }

    public string? HuntingGroundId { get; set; }

    public DateTime ActionDate { get; set; }

    public bool IsShot { get; set; }

    public bool IsActive { get; set; }

    public int AnimalId { get; set; }

    public int CreatedBy { get; set; }

    public DateTimeOffset DateExported { get; set; }

    public bool IsActionAtForest { get; set; }

    public int PrActionId { get; set; }

    public bool IsArchived { get; set; }

    public int TeamId { get; set; }

    public int UnitId { get; set; }

    public int? LastUpdatedBy { get; set; }

    public DateTimeOffset? LastUpdatedDate { get; set; }

    public int? MappedObsId { get; set; }

    public Guid PublicRegistryId { get; set; }

    public Guid? PrMappedObsId { get; set; }

    public string? ManagementAreaId { get; set; }

    public virtual AnimalAction? Action { get; set; }

    public virtual AnimalType Animal { get; set; } = null!;
}
