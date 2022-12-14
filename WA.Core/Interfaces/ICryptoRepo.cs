using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Core.Interfaces;

public interface ICryptoRepo
{
    Task SaveCoinSingleQuotation(WA.Core.OutputModels.CoinSingleResponse response, string userId);

    Task<List<WA.Core.OutputModels.CryptoDataHistory>> GetLastSaved(string userPhoneNumber = null, string coinCode = null, int num = 20);
}
