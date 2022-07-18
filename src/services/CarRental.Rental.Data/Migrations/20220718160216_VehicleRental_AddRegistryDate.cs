using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Rental.Data.Migrations
{
    public partial class VehicleRental_AddRegistryDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegistryDate",
                table: "VehicleRentals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistryDate",
                table: "ReturnInspections",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistryDate",
                table: "VehicleRentals");

            migrationBuilder.DropColumn(
                name: "RegistryDate",
                table: "ReturnInspections");
        }
    }
}
