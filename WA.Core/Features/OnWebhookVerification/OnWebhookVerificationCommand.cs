using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Core.Features.OnWebhookVerification;

public class OnWebhookVerificationCommand : IRequest<string>
{
    public HttpRequest HttpRequest { get; private set; }

    public OnWebhookVerificationCommand(HttpRequest request)
    {
        HttpRequest = request;
    }
}
