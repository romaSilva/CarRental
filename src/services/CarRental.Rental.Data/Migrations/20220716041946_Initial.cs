using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Rental.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleRentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    VehicleId = table.Column<Guid>(nullable: false),
                    CustomerName = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cpf = table.Column<string>(type: "varchar(11)", nullable: false),
                    RentDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    PlateNumber = table.Column<string>(type: "varchar(11)", nullable: false),
                    Model = table.Column<string>(type: "varchar(50)", nullable: false),
                    Year = table.Column<string>(type: "varchar(100)", nullable: true),
                    HourValue = table.Column<double>(nullable: false),
                    InitialTotalValue = table.Column<double>(nullable: false),
                    AdditionalValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleRentals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReturnInspections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OperatorId = table.Column<Guid>(nullable: false),
                    Dirty = table.Column<bool>(nullable: false),
                    EmptyTank = table.Column<bool>(nullable: false),
                    Deformed = table.Column<bool>(nullable: false),
                    Scratched = table.Column<bool>(nullable: false),
                    RentalId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnInspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnInspections_VehicleRentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "VehicleRentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReturnInspections_RentalId",
                table: "ReturnInspections",
                column: "RentalId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnInspections");

            migrationBuilder.DropTable(
                name: "VehicleRentals");
        }
    }
}
