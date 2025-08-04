using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class HuntingTeamStayInfo
{
    public int HuntingTeamStayInfoId { get; set; }

    public string HuntingTeamStayInfomation { get; set; } = null!;

    public string? OtherInfo { get; set; }

    public int UnitId { get; set; }

    public int Season { get; set; }

    public int CreatedBy { get; set; }

    public int? EditedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? EditedDate { get; set; }

    public virtual Unit Unit { get; set; } = null!;
}
