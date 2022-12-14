using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA.Core.Interfaces;
using WA.Infrastructure.Services;
using MediatR;
using WA.Infrastructure.Repositories;

namespace WA.Infrastructure.Config.Extensions;

public static class AddWACoreServicesExtension
{
    public static void AddWACoreServices(this IServiceCollection services)
    {
        services.AddTransient<IWAMessageService, WAMessageService>();

        services.AddTransient<ICryptoService, CryptoService>();

        services.AddTransient<ICryptoRepo, CryptoRepo>();

        services.AddTransient<IWAConfigurationService, WAConfigurationService>();


        services.AddMediatR(
            typeof(WA.Core.Features.OnWebhookVerification.OnWebhookVerificationCommand),
            typeof(WA.Core.Features.OnWebhookVerification.OnWebhookVerificationCommandHandler));

        services.AddMediatR(
            typeof(WA.Core.Features.OnWAMessageReceived.OnWAMessageReceivedCommand),
            typeof(WA.Core.Features.OnWAMessageReceived.OnWAMessageReceivedCommandHandler));

        services.AddMediatR(
            typeof(WA.Core.ErrorCommands.InternalServerErrorCommand),
            typeof(WA.Core.ErrorCommands.InternalServerErrorCommandHandler));
    }
}
