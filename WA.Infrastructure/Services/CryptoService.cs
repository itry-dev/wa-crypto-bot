using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WA.Core.Interfaces;
using WA.Core.Logging;
using WA.Core.OutputModels;
using WA.Infrastructure.Config;

namespace WA.Infrastructure.Services;

public class CryptoService : ICryptoService
{
    private readonly LiveCointWatchApiProviderConfig _apiProviderConfig;
    private readonly ILogger _logger;

    public CryptoService(ILoggerFactory loggerFactory, IOptions<LiveCointWatchApiProviderConfig> apiProviderConfigOptions)
    {
        _apiProviderConfig = apiProviderConfigOptions.Value;
        _logger = loggerFactory.CreateLogger<CryptoService>();
    }

    #region _getHttpClient
    private HttpClient _getHttpClient()
    {
        var cl = new HttpClient(new LoggingHandler(new HttpClientHandler(), _logger));
        cl.BaseAddress = new Uri(_apiProviderConfig.BaseEndPoint);
        cl.DefaultRequestHeaders.Add(_apiProviderConfig.HeaderKeyName, _apiProviderConfig.HeaderKeyValue);
        cl.DefaultRequestHeaders
          .Accept
          .Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return cl;
    }
    #endregion

    #region GetCrypto
    public async Task<CoinSingleResponse> GetCrypto(string code)
    {
        var message = new
        {
            code = code,
            currency = "EUR",
            meta = true
        };

        var response = await _getHttpClient().PostAsJsonAsync(_apiProviderConfig.SingleDataEndPoint, message).ConfigureAwait(false);

        var serverResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return new CoinSingleResponse
            {
                ProviderError = serverResponse
            };
        }

        var obj = Deserialize(serverResponse);
        obj.CoinCode = code;

        return obj;
    }
    #endregion

    #region Deserialize
    public CoinSingleResponse Deserialize(string json)
    {
        return System.Text.Json.JsonSerializer.Deserialize<CoinSingleResponse>(json);
    }
    #endregion

    #region GetMaxAvailbleCalls
    public async Task<int> GetMaxAvailbleCalls()
    {
        var response = await _getHttpClient().PostAsync(_apiProviderConfig.ApiDailyUsageEndPoint, 
                        new StringContent("",System.Text.Encoding.UTF8,"application/json"))
                        .ConfigureAwait(false);

        var serverResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return -1;
        }

        var obj = System.Text.Json.JsonSerializer.Deserialize<ApiDailyUsageResponse>(serverResponse);

        return obj.DailyCreditsRemaining;
    }
    #endregion
}
