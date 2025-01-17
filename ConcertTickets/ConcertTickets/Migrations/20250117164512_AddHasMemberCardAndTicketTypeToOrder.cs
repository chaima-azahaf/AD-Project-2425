using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertTickets.Migrations
{
    /// <inheritdoc />
    public partial class AddHasMemberCardAndTicketTypeToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasMemberCard",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TicketType",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasMemberCard",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TicketType",
                table: "Orders");
        }
    }
}
