using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA.Infrastructure.Config;

namespace TestProject;

public class BaseClass
{
    protected ILoggerFactory GetLoggerFactory()
    {
        return new NullLoggerFactory();
    }

    protected IOptions<LiveCointWatchApiProviderConfig> GetApiProviderConfig()
    {
        var config = new LiveCointWatchApiProviderConfig();
        return Options.Create(config);
    }
}
