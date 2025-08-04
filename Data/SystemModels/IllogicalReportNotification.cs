using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class IllogicalReportNotification
{
    public int IllogicalNotificationId { get; set; }

    public int NotifiedActionId { get; set; }

    public DateOnly NotificationDate { get; set; }

    public virtual ICollection<IllogicalReportNotifiedUser> IllogicalReportNotifiedUsers { get; set; } = new List<IllogicalReportNotifiedUser>();
}
