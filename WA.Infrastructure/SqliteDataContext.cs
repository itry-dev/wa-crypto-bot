using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WA.Infrastructure.Models;

namespace WA.Infrastructure;

public class SqliteDataContext : DbContext
{
    //public string DbPath { get; }

    private readonly IConfiguration _configuration;

    public SqliteDataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<CryptoDataHistory> CryptoDataHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CryptoDataHistory>().Property(p => p.Rate).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<CryptoDataHistory>().Property(p => p.MarketCap).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<CryptoDataHistory>().Property(p => p.Volume).HasColumnType("decimal(18,2)");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultSqlConnection"));

#if DEBUG
        optionsBuilder
            .UseLoggerFactory(MyLoggerFactory)
            .EnableSensitiveDataLogging();
#endif


    }

    public static readonly ILoggerFactory MyLoggerFactory
        = LoggerFactory.Create(builder => { builder.AddConsole(); });

}
