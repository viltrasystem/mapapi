using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class PublicRegistryHunterValidationAuthentication
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }
}
