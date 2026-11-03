using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AuthTokens2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthTokens_Users_UserId1",
                table: "AuthTokens");

            migrationBuilder.DropIndex(
                name: "IX_AuthTokens_UserId1",
                table: "AuthTokens");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AuthTokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "AuthTokens",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthTokens_UserId1",
                table: "AuthTokens",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthTokens_Users_UserId1",
                table: "AuthTokens",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
