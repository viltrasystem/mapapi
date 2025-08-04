using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class RefreshToken
{
    public Guid Token { get; set; }

    public string JwtId { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public bool Used { get; set; }

    public bool Invalidated { get; set; }

    public int UserId { get; set; }
}
