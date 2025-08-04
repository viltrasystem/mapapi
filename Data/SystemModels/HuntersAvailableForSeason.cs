using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class HuntersAvailableForSeason
{
    public int HunterSeasonId { get; set; }

    public int TeamId { get; set; }

    public int? HunterDnnId { get; set; }

    public int Season { get; set; }

    public bool IsActive { get; set; }

    public bool IsGuestHunter { get; set; }

    public string HunterFirstName { get; set; } = null!;

    public string HunterLastName { get; set; } = null!;

    public int? HunterId { get; set; }

    public int? CreatedBy { get; set; }

    public int? EditedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? EditedDate { get; set; }

    public bool IsArchive { get; set; }

    public DateTime? BirthDay { get; set; }

    public int FeePayedType { get; set; }

    public virtual Unit Team { get; set; } = null!;
}
