using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class MailBoxUsersMail
{
    public int ReceivedMailId { get; set; }

    public int MessageId { get; set; }

    public int OwnerDnnId { get; set; }

    public DateTimeOffset ReceivedOn { get; set; }

    public bool IsRead { get; set; }

    public bool IsActive { get; set; }

    public int FolderId { get; set; }

    public bool IsModified { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public int? OwnerUnitId { get; set; }

    public bool IsInfo { get; set; }

    public bool IsDocSign { get; set; }

    public virtual MailBoxFolder Folder { get; set; } = null!;

    public virtual MailBoxMessage Message { get; set; } = null!;
}
