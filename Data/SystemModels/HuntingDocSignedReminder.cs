using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class HuntingDocSignedReminder
{
    public int DocSignedReminderId { get; set; }

    public int ReminderUnitId { get; set; }

    public DateTime ReminderSendDate { get; set; }

    public int InfoId { get; set; }

    public bool IsSms { get; set; }

    public bool IsMail { get; set; }

    public int DnnUserId { get; set; }

    public int Season { get; set; }

    public bool IsHunter { get; set; }
}
