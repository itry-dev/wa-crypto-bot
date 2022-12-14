using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA.Core.OutputModels;

namespace WA.Infrastructure.Repositories;

public class CryptoRepo : WA.Core.Interfaces.ICryptoRepo
{
    private readonly ILogger _logger;
    private readonly SqliteDataContext _dataContext;

    public CryptoRepo(ILoggerFactory loggerFactory, SqliteDataContext dataContext)
    {
        _logger = loggerFactory.CreateLogger<CryptoRepo>();
        _dataContext = dataContext;
    }

    #region GetLastSaved
    public async Task<List<CryptoDataHistory>> GetLastSaved(string userPhoneNumber = null, string coinCode = null, int num = 20)
    {
        var query = _dataContext.CryptoDataHistories.AsQueryable();
        if (!string.IsNullOrEmpty(userPhoneNumber))
        {
            query = query.Where(w => w.UserId == userPhoneNumber);
        }

        if (!string.IsNullOrEmpty(coinCode))
        {
            query = query.Where(w => w.CoinCode.ToLower() == coinCode.ToLower());
        }

        return await query.Take(num)
            .Select(s => new CryptoDataHistory
            {
                Name = s.CoinName,
                Code = s.CoinCode,
                Rate = s.Rate.HasValue ? s.Rate.Value : 0,
                RecordedAtDate = s.RateDate
            })
            .ToListAsync().ConfigureAwait(false);
    } 
    #endregion

    #region SaveCoinSingleQuotation
    public async Task SaveCoinSingleQuotation(CoinSingleResponse response, string userId)
    {

        if (response == null)
        {
            _logger.LogError($"Cannot store {nameof(CoinSingleResponse)} because it's null");
            return;
        }

        _logger.LogInformation($"Storing {response} to db");

        _dataContext.CryptoDataHistories.Add(new Models.CryptoDataHistory
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CoinName = response.Name,
            CoinCode = response.CoinCode,
            Rank = response.Rank.Value,
            Rate = response.Rate.Value,
            Volume = response.Volume.Value,
            MarketCap = response.Cap.Value,
            RateDate = DateTime.UtcNow
        });

        await _dataContext.SaveChangesAsync()
            .ConfigureAwait(false);
    } 
    #endregion
}
