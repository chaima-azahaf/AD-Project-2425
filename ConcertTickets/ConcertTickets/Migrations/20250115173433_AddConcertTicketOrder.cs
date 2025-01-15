using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertTickets.Migrations
{
    /// <inheritdoc />
    public partial class AddConcertTicketOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Concerts",
                columns: new[] { "Id", "Artist", "Date", "Location" },
                values: new object[,]
                {
                    { 4, "Compact Disk Dummies", new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ancienne Belgique, Brussel" },
                    { 5, "Compact Disk Dummies", new DateTime(2025, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ancienne Belgique, Brussel" },
                    { 6, "Coldplay", new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sportpaleis, Antwerpen" },
                    { 7, "Dua Lipa", new DateTime(2025, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Werchter" },
                    { 8, "Dua Lipa", new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Werchter" }
                });

            migrationBuilder.InsertData(
                table: "TicketOffers",
                columns: new[] { "Id", "ConcertId", "NumTickets", "Price", "TicketType" },
                values: new object[,]
                {
                    { 3, 1, 60, 60.0, "Seated" },
                    { 4, 2, 1000, 200.0, "Golden Circle" },
                    { 5, 2, 19000, 50.0, "Standing" },
                    { 6, 2, 20000, 60.0, "Seated" },
                    { 7, 3, 0, 100.0, "Golden Circle" },
                    { 8, 3, 0, 28.0, "Standing" },
                    { 9, 3, 0, 32.0, "Seated" },
                    { 10, 4, 2000, 28.0, "Standing" },
                    { 11, 4, 1800, 32.0, "Seated" },
                    { 12, 5, 2000, 28.0, "Standing" },
                    { 13, 5, 7800, 32.0, "Seated" },
                    { 14, 6, 400, 150.0, "Golden Circle" },
                    { 15, 6, 4000, 65.0, "Standing" },
                    { 16, 6, 4000, 55.0, "Seated" },
                    { 17, 7, 1000, 124.0, "Golden Circle" },
                    { 18, 7, 20000, 67.0, "Standing" },
                    { 19, 8, 2000, 36.0, "Standing" },
                    { 20, 8, 7800, 40.0, "Seated" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "TicketOffers",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
