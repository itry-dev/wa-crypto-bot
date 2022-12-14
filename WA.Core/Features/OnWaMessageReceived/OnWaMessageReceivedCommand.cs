using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Core.Features.OnWAMessageReceived;

public class OnWAMessageReceivedCommand : IRequest<Unit>
{
    public WA.Core.InputModels.WA.Root WaMessage { get; private set; }
    public OnWAMessageReceivedCommand(WA.Core.InputModels.WA.Root waMessage)
    {
        WaMessage = waMessage;
    }
}
