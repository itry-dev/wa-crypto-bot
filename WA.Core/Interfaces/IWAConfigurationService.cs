using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Core.Interfaces;

public interface IWAConfigurationService
{
    /// <summary>
    /// Verifica del webhook da parte di Facebook.
    /// </summary>
    /// <param name="inputVerificationToken"></param>
    /// <returns></returns>
    bool IsOkVerificationToken(string inputVerificationToken);
}
