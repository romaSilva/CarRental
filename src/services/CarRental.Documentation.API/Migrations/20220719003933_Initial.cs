using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Documentation.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Cpf = table.Column<string>(type: "varchar(11)", nullable: false),
                    PlateNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    Model = table.Column<string>(type: "varchar(50)", nullable: false),
                    Year = table.Column<string>(type: "varchar(4)", nullable: false),
                    RentDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    Signed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");
        }
    }
}
