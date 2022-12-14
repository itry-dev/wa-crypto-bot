using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Core.Exceptions;

public class FBInvalidAccessTokenException : Exception
{
    public FBInvalidAccessTokenException(string reason) : base(reason)
    {

    }
}
