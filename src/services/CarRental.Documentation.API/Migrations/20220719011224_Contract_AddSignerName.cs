using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Documentation.API.Migrations
{
    public partial class Contract_AddSignerName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SignerName",
                table: "Contracts",
                type: "varchar(100)",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignerName",
                table: "Contracts");
        }
    }
}
