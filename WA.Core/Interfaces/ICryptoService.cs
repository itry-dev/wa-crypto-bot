using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA.Core.OutputModels;

namespace WA.Core.Interfaces;

public interface ICryptoService
{
    /// <summary>
    /// Ricerca sul codice della crypto.
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<CoinSingleResponse> GetCrypto(string code);

    CoinSingleResponse Deserialize(string json);

    Task<int> GetMaxAvailbleCalls();
}
