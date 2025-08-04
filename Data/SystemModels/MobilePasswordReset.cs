using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class MobilePasswordReset
{
    public int MobilePasswordResetId { get; set; }

    public DateTime ResetTimeBigin { get; set; }

    public DateTime ResetTimeEnd { get; set; }

    public bool IsReset { get; set; }

    public int DnnUserId { get; set; }
}
