using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class HuntingDocSigned
{
    public int DocumentSignedId { get; set; }

    public int InfoId { get; set; }

    public int UserId { get; set; }

    public int UnitId { get; set; }

    public DateTime SignedDate { get; set; }

    public virtual UnitsInfomation Info { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
