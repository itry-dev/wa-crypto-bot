using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Infrastructure.Models;

public class CryptoDataHistory : BaseModel
{
    public string? UserId { get; set; }

    public DateTime RateDate { get; set; }

    public string CoinName { get; set; }

    public string CoinCode { get; set; }

    public decimal? Rate { get; set; }

    public int? Rank { get; set; }

    public decimal? MarketCap { get; set; }

    public decimal? Volume { get; set; }
}
