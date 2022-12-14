using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WA.Infrastructure.Migrations
{
    public partial class init_sqlite_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CryptoDataHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    RateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CoinName = table.Column<string>(type: "TEXT", nullable: false),
                    CoinCode = table.Column<string>(type: "TEXT", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Rank = table.Column<int>(type: "INTEGER", nullable: true),
                    MarketCap = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoDataHistories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoDataHistories");
        }
    }
}
