using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class Smslog
{
    public int LogId { get; set; }

    public DateTimeOffset Smsdate { get; set; }

    public string KeyWord { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Sms { get; set; } = null!;

    public string Reply { get; set; } = null!;

    public int? ActionId { get; set; }

    public int? HuntingTeamId { get; set; }

    public int? UpperUnitId { get; set; }
}
