using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class PublicRegistryAuthentication
{
    public int PrUserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public string AccessTokenExpiration { get; set; } = null!;

    public string RefreshTokenExpiration { get; set; } = null!;

    public bool IsActive { get; set; }
}
