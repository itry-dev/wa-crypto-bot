using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Core.OutputModels
{
    public class CryptoDataHistory
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime RecordedAtDate { get; set; }

        public decimal Rate { get; set; }
    }
}
