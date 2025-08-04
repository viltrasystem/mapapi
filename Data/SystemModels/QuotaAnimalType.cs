using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class QuotaAnimalType
{
    public int QuotaTypeId { get; set; }

    public string QuotaType { get; set; } = null!;

    public bool IsActive { get; set; }

    public int AnimalTypeId { get; set; }

    public int GameId { get; set; }

    public int HuntingYear { get; set; }

    public bool IsShared { get; set; }

    public int? OriginallySharedByDnnId { get; set; }

    public int? SharedFrom { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedByDnnId { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public int? LastModifiedByDnnId { get; set; }

    public virtual AnimalType AnimalType { get; set; } = null!;

    public virtual ICollection<CommonQuotaCount> CommonQuotaCounts { get; set; } = new List<CommonQuotaCount>();

    public virtual HuntingGame Game { get; set; } = null!;

    public virtual ICollection<QuotaAnimalTypeUnit> QuotaAnimalTypeUnits { get; set; } = new List<QuotaAnimalTypeUnit>();

    public virtual ICollection<QuotaCount> QuotaCounts { get; set; } = new List<QuotaCount>();

    public virtual ICollection<QuotaTypeMappedToNormalType> QuotaTypeMappedToNormalTypes { get; set; } = new List<QuotaTypeMappedToNormalType>();

    public virtual ICollection<QuotaTypesMappedToAntlersType> QuotaTypesMappedToAntlersTypes { get; set; } = new List<QuotaTypesMappedToAntlersType>();

    public virtual ICollection<QuotaTypesMappedToTag> QuotaTypesMappedToTags { get; set; } = new List<QuotaTypesMappedToTag>();

    public virtual ICollection<QuotaTypesMappedToWeight> QuotaTypesMappedToWeights { get; set; } = new List<QuotaTypesMappedToWeight>();

    public virtual Unit? SharedFromNavigation { get; set; }
}
