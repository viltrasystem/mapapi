using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class FailedNewsLetter
{
    public int FailedId { get; set; }

    public int InfoId { get; set; }

    public int ReceiverDnnId { get; set; }

    public int ReceiverUnitId { get; set; }

    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string ModifiedMailBody { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public bool IsSent { get; set; }

    public DateTime FailedDateTime { get; set; }

    public DateTime? SentDateTime { get; set; }

    public string Language { get; set; } = null!;

    public virtual UnitsInfomation Info { get; set; } = null!;
}
