using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Core.OutputModels;

public class ApiDailyUsageResponse
{
    [System.Text.Json.Serialization.JsonPropertyName("dailyCreditsRemaining")]
    public int DailyCreditsRemaining { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("dailyCreditsLimit")]
    public int DailyCreditsLimit { get; set; }
}
