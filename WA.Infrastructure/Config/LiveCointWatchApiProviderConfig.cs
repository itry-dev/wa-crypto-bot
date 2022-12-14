namespace WA.Infrastructure.Config;

public class LiveCointWatchApiProviderConfig
{
    public string Name { get; set; }

    public int ApiMaxLimitCall { get; set; }

    public string HeaderKeyName { get; set; }

    public string HeaderKeyValue { get; set; }

    public string BaseEndPoint { get; set; }

    public string SingleDataEndPoint { get; set; }

    public string SingleDataHistoryEndPoint { get; set; }

    public string ApiDailyUsageEndPoint { get; set; }
}