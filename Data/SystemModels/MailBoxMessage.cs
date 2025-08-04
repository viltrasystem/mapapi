using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class MailBoxMessage
{
    public int MessageId { get; set; }

    public int SenderDnnId { get; set; }

    public string? Subject { get; set; }

    public string BodyHtml { get; set; } = null!;

    public DateTimeOffset SentOn { get; set; }

    public bool IsActive { get; set; }

    public string BodyPlain { get; set; } = null!;

    public virtual ICollection<MailBoxUsersMail> MailBoxUsersMails { get; set; } = new List<MailBoxUsersMail>();
}
