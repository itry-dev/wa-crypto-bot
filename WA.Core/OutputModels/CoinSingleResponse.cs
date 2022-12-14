using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA.Core.OutputModels;

public class CoinSingleResponse
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    public string Name { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("rank")]
    public int? Rank { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("age")]
    public int? Age { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("allTimeHighUSD")]
    public decimal? AllTimeHighUSD { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("circulatingSupply")]
    public long? CirculatingSupply { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("totalSupply")]
    public long? TotalSupply { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("maxSupply")]
    public long? MaxSupply { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("rate")]
    public decimal? Rate { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("volume")]
    public decimal? Volume { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("cap")]
    public decimal? Cap { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("liquidity")]
    public decimal? Liquidity { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public string ProviderError { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public string CoinCode { get; set; }

    public override string ToString()
    {
        return $"Name {Name}, Code {CoinCode}, Rate {Rate}, Rank {Rank}, Cap {Cap}";
    }
}
