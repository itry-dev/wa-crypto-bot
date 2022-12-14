using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA.Core.Interfaces;

namespace WA.Core.ErrorCommands;

public class InternalServerErrorCommandHandler : IRequestHandler<InternalServerErrorCommand, Unit>
{
    private IWAMessageService _waMessageService;

    public InternalServerErrorCommandHandler(IWAMessageService waMessageService)
    {
        _waMessageService = waMessageService;
    }

    public Task<Unit> Handle(InternalServerErrorCommand request, CancellationToken cancellationToken)
    {
        _waMessageService.PostWAMessage(request.Message);

        return Task.FromResult<Unit>(Unit.Value);
    }
}
