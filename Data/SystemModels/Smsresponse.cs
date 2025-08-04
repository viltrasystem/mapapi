using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class Smsresponse
{
    public decimal Id { get; set; }

    public string DestinationAddress { get; set; } = null!;

    public string SourceAddress { get; set; } = null!;

    public string Message { get; set; } = null!;

    public bool IsSuccess { get; set; }

    public int? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }
}
