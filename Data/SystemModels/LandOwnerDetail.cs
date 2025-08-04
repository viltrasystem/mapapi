using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class LandOwnerDetail
{
    public int LandOwnerDetailId { get; set; }

    public int SystemUserId { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? AddressCity { get; set; }

    public string? BankAccountNo { get; set; }

    public string? Notes { get; set; }

    public int? LandId { get; set; }

    public bool IsArchived { get; set; }

    public virtual ViltraUser SystemUser { get; set; } = null!;
}
