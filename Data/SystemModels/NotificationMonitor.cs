using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class NotificationMonitor
{
    public int Id { get; set; }

    public int UserDnnId { get; set; }

    public int NotificationTypeId { get; set; }

    public bool IsNotified { get; set; }

    public DateTime? NotifiedDate { get; set; }

    public bool IsAccepted { get; set; }

    public DateTime? AcceptedDate { get; set; }
}
