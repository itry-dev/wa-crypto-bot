using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA.Core.Interfaces;

namespace WA.Core.Features.OnWAMessageReceived;

public class OnWAMessageReceivedCommandHandler : IRequestHandler<OnWAMessageReceivedCommand, Unit>
{
    private readonly ILogger _logger;
    private readonly IWAMessageService _waMessageService;

    public OnWAMessageReceivedCommandHandler(
        ILoggerFactory loggerFactory,
        IWAMessageService waMessageService
        )
    {
        _logger = loggerFactory.CreateLogger<OnWAMessageReceivedCommandHandler>();
        _waMessageService = waMessageService;
    }

    public async Task<Unit> Handle(OnWAMessageReceivedCommand request, CancellationToken cancellationToken)
    {
        await _waMessageService.HandleIncomingWAMessage(request.WaMessage)
                .ConfigureAwait(false);        

        return Unit.Value;
    }
}
