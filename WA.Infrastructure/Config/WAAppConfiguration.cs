using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Infrastructure.Config;

public class WAAppConfiguration
{
    public string VerificationToken { get; set; }

    public string FbAccessToken { get; set; }

    public string PhoneNumberId { get; set; }

    public string FbApiVersionId { get; set; }
}
