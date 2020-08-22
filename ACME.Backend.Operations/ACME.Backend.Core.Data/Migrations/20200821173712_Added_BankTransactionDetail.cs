using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ACME.Backend.Core.Data.Migrations
{
    public partial class Added_BankTransactionDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankTransactionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountId = table.Column<string>(nullable: false),
                    TransactionType = table.Column<string>(nullable: false),
                    TransactionAmount = table.Column<double>(nullable: false),
                    RequestedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransactionDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankTransactionDetails");
        }
    }
}
