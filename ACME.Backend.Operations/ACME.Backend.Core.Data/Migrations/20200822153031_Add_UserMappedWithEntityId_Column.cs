using Microsoft.EntityFrameworkCore.Migrations;

namespace ACME.Backend.Core.Data.Migrations
{
    public partial class Add_UserMappedWithEntityId_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserMappedWithEntityId",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserMappedWithEntityId",
                table: "Users");
        }
    }
}
