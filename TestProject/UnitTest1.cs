using WA.Infrastructure.Services;

namespace TestProject
{
    public class UnitTest1 : BaseClass
    {
        [Fact]
        public void livecoinwatch_coinsingle_response_test()
        {
            var response = "{\"rate\":27710.1666609745,\"volume\":16986341518,\"cap\":527851823740,\"liquidity\":977863738}";

            var cryptoService = new CryptoService(GetLoggerFactory(), GetApiProviderConfig());

            var singleCoinClass = cryptoService.Deserialize(response);

            Assert.True(singleCoinClass.Rate > 0);
        }
    }
}