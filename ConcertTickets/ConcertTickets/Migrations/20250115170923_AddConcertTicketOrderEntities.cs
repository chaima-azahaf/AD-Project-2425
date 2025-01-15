using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertTickets.Migrations
{
    /// <inheritdoc />
    public partial class AddConcertTicketOrderEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Artist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concerts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumTickets = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ConcertId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketOffers_Concerts_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumTickets = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    DiscountApplied = table.Column<bool>(type: "bit", nullable: false),
                    TicketOfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_TicketOffers_TicketOfferId",
                        column: x => x.TicketOfferId,
                        principalTable: "TicketOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Concerts",
                columns: new[] { "Id", "Artist", "Date", "Location" },
                values: new object[,]
                {
                    { 1, "Taylor Swift", new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Koning Boudewijn Stadion, Brussel" },
                    { 2, "Taylor Swift", new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Koning Boudewijn Stadion, Brussel" },
                    { 3, "Charli XCX", new DateTime(2025, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vorst Nationaal, Brussel" }
                });

            migrationBuilder.InsertData(
                table: "TicketOffers",
                columns: new[] { "Id", "ConcertId", "NumTickets", "Price", "TicketType" },
                values: new object[,]
                {
                    { 1, 1, 10, 200.0, "Golden Circle" },
                    { 2, 1, 50, 50.0, "Standing" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TicketOfferId",
                table: "Orders",
                column: "TicketOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketOffers_ConcertId",
                table: "TicketOffers",
                column: "ConcertId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "TicketOffers");

            migrationBuilder.DropTable(
                name: "Concerts");
        }
    }
}
