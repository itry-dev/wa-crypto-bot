using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA.Core.Interfaces;
using WA.Infrastructure.Config;

namespace WA.Infrastructure.Services;

public class WAConfigurationService : IWAConfigurationService
{
    WAAppConfiguration _waAppConfiguration;

    public WAConfigurationService(IOptions<WAAppConfiguration> options)
    {
        _waAppConfiguration = options.Value;
    }

    #region IsOkVerificationToken
    public bool IsOkVerificationToken(string inputVerificationToken)
    {
        if (string.IsNullOrEmpty(inputVerificationToken)) return false;

        if (inputVerificationToken != _waAppConfiguration.VerificationToken) return false;

        return true;
    }
    #endregion

}
