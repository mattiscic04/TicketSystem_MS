using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Tickets_TicketId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_TicketId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "ArrivalDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CommisionRate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CountryFrom",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DepartureDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "FlightIdFK",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "WholesalePrice",
                table: "Tickets",
                newName: "PricePaid");

            migrationBuilder.RenameColumn(
                name: "Rows",
                table: "Tickets",
                newName: "Row");

            migrationBuilder.RenameColumn(
                name: "CountryTo",
                table: "Tickets",
                newName: "Passport");

            migrationBuilder.RenameColumn(
                name: "Columns",
                table: "Tickets",
                newName: "Column");

            migrationBuilder.RenameColumn(
                name: "Row",
                table: "Flights",
                newName: "Rows");

            migrationBuilder.RenameColumn(
                name: "PricePaid",
                table: "Flights",
                newName: "WholesalePrice");

            migrationBuilder.RenameColumn(
                name: "Passport",
                table: "Flights",
                newName: "CountryTo");

            migrationBuilder.RenameColumn(
                name: "Column",
                table: "Flights",
                newName: "Columns");

            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "FlightIdFK",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalDate",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "CommisionRate",
                table: "Flights",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CountryFrom",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureDate",
                table: "Flights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FlightIdFK",
                table: "Tickets",
                column: "FlightIdFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Flights_FlightIdFK",
                table: "Tickets",
                column: "FlightIdFK",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Flights_FlightIdFK",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_FlightIdFK",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "FlightIdFK",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ArrivalDate",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "CommisionRate",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "CountryFrom",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DepartureDate",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "Row",
                table: "Tickets",
                newName: "Rows");

            migrationBuilder.RenameColumn(
                name: "PricePaid",
                table: "Tickets",
                newName: "WholesalePrice");

            migrationBuilder.RenameColumn(
                name: "Passport",
                table: "Tickets",
                newName: "CountryTo");

            migrationBuilder.RenameColumn(
                name: "Column",
                table: "Tickets",
                newName: "Columns");

            migrationBuilder.RenameColumn(
                name: "WholesalePrice",
                table: "Flights",
                newName: "PricePaid");

            migrationBuilder.RenameColumn(
                name: "Rows",
                table: "Flights",
                newName: "Row");

            migrationBuilder.RenameColumn(
                name: "CountryTo",
                table: "Flights",
                newName: "Passport");

            migrationBuilder.RenameColumn(
                name: "Columns",
                table: "Flights",
                newName: "Column");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalDate",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "CommisionRate",
                table: "Tickets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CountryFrom",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureDate",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "Flights",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "FlightIdFK",
                table: "Flights",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TicketId",
                table: "Flights",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Flights_TicketId",
                table: "Flights",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Tickets_TicketId",
                table: "Flights",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
