using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Core.ErrorCommands;

public class InternalServerErrorCommand : IRequest<Unit>
{
    public string Message { get; private set; }

    public InternalServerErrorCommand(string message)
    {
        Message = message;
    }
}
