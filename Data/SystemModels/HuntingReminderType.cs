using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class HuntingReminderType
{
    public int HuntingReminderTypeId { get; set; }

    public bool IsTeamLeaderMissed { get; set; }

    public bool IsDocUnSigned { get; set; }

    public bool IsSeasonListRegister { get; set; }

    public bool IsMissingHunterId { get; set; }

    public bool IsHuntingDogRegister { get; set; }

    public bool IsMissingHnutingFee { get; set; }

    public int HunterDnnId { get; set; }

    public int HuntingReminderId { get; set; }

    public bool IsHunter { get; set; }

    public int TeamId { get; set; }

    public bool IsTeamLeaderSeasonMissed { get; set; }

    public virtual HuntingReminder HuntingReminder { get; set; } = null!;
}
