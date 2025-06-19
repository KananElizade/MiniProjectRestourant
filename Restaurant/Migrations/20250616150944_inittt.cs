using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restourant.Migrations
{
    /// <inheritdoc />
    public partial class inittt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationEnd",
                table: "ReserveTables");

            migrationBuilder.RenameColumn(
                name: "ReservationStart",
                table: "ReserveTables",
                newName: "ReservationStartTime");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "ReserveTables",
                newName: "GuestPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "ReserveTables",
                newName: "GuestLastName");

            migrationBuilder.AddColumn<string>(
                name: "GuestFirstName",
                table: "ReserveTables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationEndTime",
                table: "ReserveTables",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestFirstName",
                table: "ReserveTables");

            migrationBuilder.DropColumn(
                name: "ReservationEndTime",
                table: "ReserveTables");

            migrationBuilder.RenameColumn(
                name: "ReservationStartTime",
                table: "ReserveTables",
                newName: "ReservationStart");

            migrationBuilder.RenameColumn(
                name: "GuestPhoneNumber",
                table: "ReserveTables",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "GuestLastName",
                table: "ReserveTables",
                newName: "FullName");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationEnd",
                table: "ReserveTables",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
