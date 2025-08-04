using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class MailBoxFolder
{
    public int FolderId { get; set; }

    public string FolderName { get; set; } = null!;

    public int OwnerDnnId { get; set; }

    public bool IsPublic { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsInbox { get; set; }

    public bool IsSentItems { get; set; }

    public bool IsDeletedItems { get; set; }

    public virtual ICollection<MailBoxUsersMail> MailBoxUsersMails { get; set; } = new List<MailBoxUsersMail>();
}
