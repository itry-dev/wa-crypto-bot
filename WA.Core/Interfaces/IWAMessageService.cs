using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA.Core.OutputModels;

namespace WA.Core.Interfaces;

public interface IWAMessageService
{
    Task HandleIncomingWAMessage(WA.Core.InputModels.WA.Root waMessage);

    Task PostWAMessage(string message);

    Task MarkMessageAsRead(string messageId);

    Task AskSaveCoinRate(string messageFor);
}
