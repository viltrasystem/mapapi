using System;
using System.Collections.Generic;

namespace ViltrapportenApi.Data.SystemModels;

public partial class ReportImageDownloadLog
{
    public int ImageLogId { get; set; }

    public int ImageId { get; set; }

    public DateTimeOffset DownloadedDateTime { get; set; }

    public int DnnUserId { get; set; }
}
