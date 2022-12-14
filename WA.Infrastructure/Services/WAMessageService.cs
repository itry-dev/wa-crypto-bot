using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WA.Core;
using WA.Core.Exceptions;
using WA.Core.Interfaces;
using WA.Core.Logging;
using WA.Core.OutputModels.WA;
using WA.Infrastructure.Config;

namespace WA.Infrastructure.Services;

public class WAMessageService : WA.Core.Interfaces.IWAMessageService
{
    private readonly ILogger _logger;
    private readonly ICryptoService _cryptoService;
    private readonly ICryptoRepo _cryptoRepo;
    private readonly WAAppConfiguration _waAppConfiguration;

    public WAMessageService(ILoggerFactory loggerFactory, 
        ICryptoService cryptoService, 
        ICryptoRepo cryptoRepo,
        IOptions<WAAppConfiguration> configOptions)
    {
        _logger = loggerFactory.CreateLogger<WAMessageService>();
        _cryptoService = cryptoService;
        _cryptoRepo = cryptoRepo;
        _waAppConfiguration = configOptions.Value;
    }

    #region HandleIncomingWAMessage
    public async Task HandleIncomingWAMessage(WA.Core.InputModels.WA.Root waMessage)
    {

        var messages = waMessage.entry?[0].changes?[0].value.messages;
        var messageTo = waMessage.entry?[0].changes?[0].value.contacts?[0].wa_id;

        if (messages == null || messages.Count == 0 || string.IsNullOrEmpty(messageTo))
        {
            _logger.LogInformation($"Not a message. Server replied with {System.Text.Json.JsonSerializer.Serialize(waMessage)}");
            return;
        }


        #region mark message as read
        await MarkMessageAsRead(messages[0].id).ConfigureAwait(false);
        #endregion

        if (messages[0].text != null) 
        {
            bool storeQuotation = false;
            var message = messages[0].text.body;
            var tokens = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var action = tokens[0].ToLower().Trim();

            if (action != "get" && action != "last")
            {
                await _sendBotGuide(messageTo).ConfigureAwait(false);
                return;
            }

            #region handling the message
            var code = tokens[1].ToUpper();
            if (action == "get" && tokens.Length == 3)
            {
                if (tokens[2] == "-s") storeQuotation = true;
            }

            if (action == "get")
            {
                #region sending a feedback message
                await PostWAMessage(WATextMessageTemplate.GetWATextMessageTemplate(messageTo,
                    "Ok, you asked for " + code + ", " + Environment.NewLine + (storeQuotation ? "data will be saved" : "quotation will not be saved") + Environment.NewLine+"give a second and I'll be back to you"))
                    .ConfigureAwait(false);
                #endregion

                #region calling crypto provider
                var coinSingleResponse = await _cryptoService.GetCrypto(code).ConfigureAwait(false);
                #endregion

                #region sending back response
                var outResponse = "";
                if (string.IsNullOrEmpty(coinSingleResponse.ProviderError))
                {
                    outResponse = coinSingleResponse.Name + ", rate: ";

                    if (coinSingleResponse.Rate.HasValue)
                    {
                        outResponse += coinSingleResponse.Rate?.ToString("F")+" "+TextUtils.GetEuroSign();
                    }
                    else
                    {
                        outResponse += "(n.a.)";
                    }

                    outResponse += ", rank: ";

                    if (coinSingleResponse.Rank.HasValue)
                    {
                        outResponse += coinSingleResponse.Rank.Value;
                    }
                    else
                    {
                        outResponse += "(n.a.)";
                    }
                }
                else
                {
                    outResponse = coinSingleResponse.ProviderError;
                }

                await PostWAMessage(WATextMessageTemplate.GetWATextMessageTemplate(messageTo, outResponse))
                    .ConfigureAwait(false);
                #endregion

                #region sending back api coin provider max limit calls
                var maxAvailableCalls = await _cryptoService.GetMaxAvailbleCalls()
                                                    .ConfigureAwait(false);
                await PostWAMessage(WATextMessageTemplate.GetWATextMessageTemplate(messageTo, maxAvailableCalls == -1 ? "Error trying getting max calls remaining" : "Available livecoinwatch calls for today " + maxAvailableCalls.ToString()))
                    .ConfigureAwait(false);
                #endregion

                #region save this call?
                if (storeQuotation)
                {
                    await _cryptoRepo.SaveCoinSingleQuotation(coinSingleResponse, messageTo)
                        .ConfigureAwait(false);

                    await PostWAMessage(WATextMessageTemplate.GetWATextMessageTemplate(messageTo, "Quotation recorded"+TextUtils.GetExclamationMark()))
                       .ConfigureAwait(false);
                }
                //await AskSaveCoinRate(messageTo).ConfigureAwait(false);
                #endregion
            }
            else
            {
                var records = await _cryptoRepo.GetLastSaved(messageTo, code)
                                .ConfigureAwait(false);

                if (records == null || records.Count == 0)
                {
                    await PostWAMessage(WATextMessageTemplate.GetWATextMessageTemplate(messageTo, "No data saved yet"))
                       .ConfigureAwait(false);
                }
                else
                {
                    var msg = new System.Text.StringBuilder();
                    foreach (var record in records)
                    {
                        msg.AppendLine($"CoinName: {record.Name}, CoinCode: {record.Code}, Rate: {record.Rate.ToString("F")} {TextUtils.GetEuroSign()}, Date: {record.RecordedAtDate}{Environment.NewLine}");
                    }

                    await PostWAMessage(WATextMessageTemplate.GetWATextMessageTemplate(messageTo, msg.ToString()))
                       .ConfigureAwait(false);
                }
            }
            #endregion
        }
        else
        {
            //no op per ora
            _logger.LogInformation($"Not a message, not a button click. Server replied with {System.Text.Json.JsonSerializer.Serialize(waMessage)}");
        }

        
    }
    #endregion

    #region AskSaveCoinRate
    public async Task AskSaveCoinRate(string messageFor)
    {
        //save_crypto_quotation_rate
        //en_UK
        var template = new
        {
            messaging_product = "whatsapp",
            recipient_type = "individual",
            to = messageFor,
            type = "template",
            template = new
            {
                name = "remember_boiler_counter",
                language = new 
                {
                    code = "it"
                },
                components = new[]
                {
                    new
                    {
                        type = "body",
                        parameters = new[]
                        {
                            new
                            {
                                type = "text",
                                text = messageFor
                            },
                            new
                            {
                                type = "text",
                                text = messageFor
                            }
                        }
                    }                    
                }                
            }
        };

        await PostWAMessage(System.Text.Json.JsonSerializer.Serialize(template))
                .ConfigureAwait(false);

    }
    #endregion

    #region PostWAMessage
    public async Task PostWAMessage(string message)
    {

        using (var client = _getHttpClient())
        {

            using (var response = await client
                                    .PostAsync($"/{_waAppConfiguration.PhoneNumberId}/messages", 
                                    new StringContent(message, System.Text.Encoding.UTF8, "application/json"))
                                    .ConfigureAwait(false))
            {
                var serverResponse = await response.Content
                                    .ReadAsStringAsync()
                                    .ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    _logger.LogCritical($"Check that the access token is never ending token", serverResponse);
                    throw new FBInvalidAccessTokenException(serverResponse);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogDebug(serverResponse);
                }
                else
                {
                    _logger.LogError($"Server replied with {response.StatusCode} code, error sending message: {serverResponse}");
                }
            }
        }
    }
    #endregion

    #region MarkMessageAsRead
    public async Task MarkMessageAsRead(string messageId)
    {
        var message = new
        {
            messaging_product = "whatsapp",
            status = "read",
            message_id = messageId
        };

        using (var client = _getHttpClient())
        {
            using (var response = await client
                                    .PostAsJsonAsync($"/{_waAppConfiguration.PhoneNumberId}/messages", message)
                                    .ConfigureAwait(false))
            {
                var serverResponse = await response.Content
                                    .ReadAsStringAsync()
                                    .ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogDebug($"Message mark as read: {serverResponse}");
                }
                else
                {
                    _logger.LogError($"Cannot mark message {messageId} as read, server replied with {response.StatusCode} code, error sending message: {serverResponse}");
                }
            }
        }
    }
    #endregion

    #region _sendBotGuide
    private async Task _sendBotGuide(string messageFor)
    {
        await PostWAMessage(WATextMessageTemplate.GetWATextMessageTemplate(messageFor, 
            "To get a single coin history type GET {coin code} where coin code is the crypto your looking for." 
            + Environment.NewLine + "If you want to store the obtained response add -s after coin code." 
            + Environment.NewLine + "To get the last recorded responses type LAST {coin code}"))
            .ConfigureAwait(false);
    }
    #endregion

    #region _getHttpClient
    private HttpClient _getHttpClient()
    {
        var httpCl = new HttpClient(new LoggingHandler(new HttpClientHandler(), _logger));
        httpCl.BaseAddress = new Uri($"https://graph.facebook.com/{_waAppConfiguration.FbApiVersionId}");
        httpCl.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _waAppConfiguration.FbAccessToken);
        return httpCl;
    }
    #endregion
}
