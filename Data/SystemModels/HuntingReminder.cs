using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class HuntingReminder
{
    public int HuntingReminderId { get; set; }

    public int SenderDnnId { get; set; }

    public DateTime SendDateTime { get; set; }

    public int Season { get; set; }

    public int SenderUnitId { get; set; }

    public bool IsEmail { get; set; }

    public bool IsSms { get; set; }

    public virtual ICollection<HuntingReminderType> HuntingReminderTypes { get; set; } = new List<HuntingReminderType>();
}
