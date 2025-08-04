using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class Log
{
    public int Id { get; set; }

    public DateTimeOffset Date { get; set; }

    public string LogType { get; set; } = null!;

    public int? UserId { get; set; }

    public string? Page { get; set; }

    public string? Method { get; set; }

    public string? Message { get; set; }

    public string? StackTrace { get; set; }

    public string? AdditionalInfo { get; set; }
}
