using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class AnimalActionAnimalDetail
{
    public int AnimalDetailsId { get; set; }

    public int ActionId { get; set; }

    public float? CordinateX { get; set; }

    public float? CordinateY { get; set; }

    public string? Tags { get; set; }

    public int? AntlersType { get; set; }

    public int? HuntingMethod { get; set; }

    public float? Age { get; set; }

    public int? AgeIsEstimated { get; set; }

    public float? AnimalWeight { get; set; }

    public int? WeightIsEstimated { get; set; }

    public string? HairLoss { get; set; }

    public string? Hjortelusflue { get; set; }

    public string? Ticks { get; set; }

    public int? AnimalSubTypeId { get; set; }

    public DateTime? DateWeighted { get; set; }

    public string? VetTestId { get; set; }

    public int? HuntingPoliceUserId { get; set; }

    public DateTime? HpCommentDate { get; set; }

    public string? ApprovedorNotApproved { get; set; }

    public string? HpComment { get; set; }

    public float? PriceWeight { get; set; }

    public int? PriceWeightLastUpdatedBy { get; set; }

    public DateTimeOffset? PriceWeightLastUpdatedDateTime { get; set; }

    public int? WeightingMethodId { get; set; }

    public virtual AnimalAction Action { get; set; } = null!;

    public virtual ICollection<AnimalDetailsWeightChangedLog> AnimalDetailsWeightChangedLogs { get; set; } = new List<AnimalDetailsWeightChangedLog>();

    public virtual AnimalSubType? AnimalSubType { get; set; }
}
