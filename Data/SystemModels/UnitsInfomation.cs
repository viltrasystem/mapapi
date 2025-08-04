using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class UnitsInfomation
{
    public int InfoId { get; set; }

    public int UserDnnId { get; set; }

    public int UnitId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime InfoDate { get; set; }

    public string Summary { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsNewsLetter { get; set; }

    public bool IsOnWindow { get; set; }

    public bool IsAllLandOwners { get; set; }

    public bool IsOtherthanHt { get; set; }

    public bool IsAllTeam { get; set; }

    public int Sequence { get; set; }

    public int InfoTypeId { get; set; }

    public bool IsSelectChildren { get; set; }

    public int? ModifiedUserDnnId { get; set; }

    public bool IsInactiveUser { get; set; }

    public bool IsDraft { get; set; }

    public bool HasUnsent { get; set; }

    public bool IsTeamLeader { get; set; }

    public bool IsDocSign { get; set; }

    public int? ReminderDays { get; set; }

    public int? IsReminderMail { get; set; }

    public int? IsReminderSms { get; set; }

    public DateTime? ReminderCreatedDate { get; set; }

    public virtual ICollection<FailedNewsLetter> FailedNewsLetters { get; set; } = new List<FailedNewsLetter>();

    public virtual ICollection<HuntingDocSigned> HuntingDocSigneds { get; set; } = new List<HuntingDocSigned>();

    public virtual InfoType InfoType { get; set; } = null!;

    public virtual ICollection<InfoUnit> InfoUnits { get; set; } = new List<InfoUnit>();

    public virtual ICollection<InfoUser> InfoUsers { get; set; } = new List<InfoUser>();

    public virtual Unit Unit { get; set; } = null!;
}
