using Microsoft.EntityFrameworkCore.Migrations;

namespace SSKCurrencySync.Migrations
{
    public partial class addingAllowSyncAndLastSync : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowSync",
                table: "Conversion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastSync",
                table: "Conversion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowSync",
                table: "Conversion");

            migrationBuilder.DropColumn(
                name: "LastSync",
                table: "Conversion");
        }
    }
}
