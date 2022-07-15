using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Fleet.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlateNumber = table.Column<string>(type: "varchar(10)", nullable: false),
                    Year = table.Column<string>(type: "varchar(4)", nullable: false),
                    HourValue = table.Column<double>(nullable: false),
                    BaggageSize = table.Column<double>(nullable: false),
                    Brand = table.Column<string>(type: "varchar(50)", nullable: false),
                    Model = table.Column<string>(type: "varchar(50)", nullable: false),
                    Category = table.Column<int>(nullable: false),
                    Fuel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
