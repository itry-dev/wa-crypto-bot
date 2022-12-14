using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA.Infrastructure.Config;

namespace WA.Infrastructure.Config.Extensions;

public static class AddWACoreConfigExtensions
{
    public static void AddWACoreConfig(this IServiceCollection services)
    {
        services.AddOptions<LiveCointWatchApiProviderConfig>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                string root = "ApiProviders:LiveCoinWatch";

                settings.Name = configuration.GetSection(root + ":Name").Get<string>();

                settings.ApiMaxLimitCall = configuration.GetSection(root + ":ApiMaxLimitCall").Get<int>();


                settings.HeaderKeyName = configuration.GetSection(root + ":HeaderKeyName").Get<string>();

                settings.HeaderKeyValue = configuration.GetSection(root + ":HeaderKeyValue").Get<string>();

                settings.BaseEndPoint = configuration.GetSection(root + ":BaseEndPoint").Get<string>();

                settings.SingleDataEndPoint = configuration.GetSection(root + ":SingleDataEndPoint").Get<string>();

                settings.SingleDataHistoryEndPoint = configuration.GetSection(root + ":SingleDataHistoryEndPoint").Get<string>();

                settings.ApiDailyUsageEndPoint = configuration.GetSection(root + ":ApiDailyUsageEndPoint").Get<string>();

            });


        services.AddOptions<WAAppConfiguration>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                string root = "WhatsAppFbConfiguration";

                settings.VerificationToken = configuration.GetSection(root + ":VerificationToken").Get<string>();

                settings.FbAccessToken = configuration.GetSection(root + ":FbAccessToken").Get<string>();

                settings.PhoneNumberId = configuration.GetSection(root + ":PhoneNumberId").Get<string>();

                settings.FbApiVersionId = configuration.GetSection(root + ":FbApiVersionId").Get<string>();
            });
    }
}
