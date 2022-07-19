using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Documentation.API.Migrations
{
    public partial class Contract_AddRentalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RentalId",
                table: "Contracts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalId",
                table: "Contracts");
        }
    }
}
