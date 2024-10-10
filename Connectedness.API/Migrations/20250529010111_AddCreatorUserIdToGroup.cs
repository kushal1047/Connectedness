using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectedness.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatorUserIdToGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatedByUserId",
                table: "Groups",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_CreatedByUserId",
                table: "Groups",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_CreatedByUserId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CreatedByUserId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Groups");
        }
    }
}
