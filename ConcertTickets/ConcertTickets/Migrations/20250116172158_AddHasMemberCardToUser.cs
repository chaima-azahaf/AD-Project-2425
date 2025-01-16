using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertTickets.Migrations
{
    /// <inheritdoc />
    public partial class AddHasMemberCardToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfTickets",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasMemberCard",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfTickets",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "HasMemberCard",
                table: "AspNetUsers");
        }
    }
}
