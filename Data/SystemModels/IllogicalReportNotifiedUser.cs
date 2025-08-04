using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class IllogicalReportNotifiedUser
{
    public int IllogicalReportNotifiedUserId { get; set; }

    public int DnnUserId { get; set; }

    public int IllogicalNotificationId { get; set; }

    public virtual IllogicalReportNotification IllogicalNotification { get; set; } = null!;
}
