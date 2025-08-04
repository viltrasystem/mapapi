using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class DistributionSmsLog
{
    public int LogId { get; set; }

    public DateTimeOffset Smsdate { get; set; }

    public string KeyWord { get; set; } = null!;

    public string? SenderNumber { get; set; }

    public string RecieverNumber { get; set; } = null!;

    public string Message { get; set; } = null!;

    public int? ActionId { get; set; }

    public int? HuntingTeamId { get; set; }

    public int? ReceiverId { get; set; }
}
