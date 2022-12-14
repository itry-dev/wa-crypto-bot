using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA.Core.Interfaces;

namespace WA.Core.Features.OnWebhookVerification;

public class OnWebhookVerificationCommandHandler : IRequestHandler<OnWebhookVerificationCommand, string>
{
    IWAConfigurationService _waAppConfigService;

    public OnWebhookVerificationCommandHandler(IWAConfigurationService waAppConfigService)
    {
        _waAppConfigService = waAppConfigService;
    }

    public Task<string> Handle(OnWebhookVerificationCommand request, CancellationToken cancellationToken)
    {
        if (!_waAppConfigService.IsOkVerificationToken(request.HttpRequest.Query["hub.verify_token"]))
        {
            return Task.FromResult(String.Empty);
        }

        return Task.FromResult<string>(request.HttpRequest.Query["hub.challenge"]);
    }

}
