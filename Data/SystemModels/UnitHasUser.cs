using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class UnitHasUser
{
    public int UnitHasUsersId { get; set; }

    public int UnitId { get; set; }

    public int UserId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public string? UnitUserName { get; set; }

    public bool IsHead { get; set; }

    public bool IsReporter { get; set; }

    public bool IsMainUnit { get; set; }

    public bool IsTeamLeader { get; set; }

    public bool? IsUnitUserNameChangeMailSent { get; set; }

    public bool IsMunicipalityUser { get; set; }

    public bool IsGuest { get; set; }

    public bool IsExporter { get; set; }

    public bool IsHuntingPolice { get; set; }

    public bool IsPriceUser { get; set; }

    public int? LastUpdatedBy { get; set; }

    public DateTime? LastUpdatedDate { get; set; }

    public virtual Unit Unit { get; set; } = null!;

    public virtual ViltraUser User { get; set; } = null!;
}
