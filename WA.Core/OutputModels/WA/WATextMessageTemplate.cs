using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Core.OutputModels.WA;

public class WATextMessageTemplate
{
    /// <summary>
    /// Ritorna il json da inviare per messaggi di tipo testo.
    /// </summary>
    /// <param name="to"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static string GetWATextMessageTemplate(string to, string message)
    {
        var theTextMessage = new
        {
            messaging_product = "whatsapp",
            to = to,
            text = new
            {
                body = message,
                preview_url = false
            }
        };

        return System.Text.Json.JsonSerializer.Serialize(theTextMessage);
    }
}
