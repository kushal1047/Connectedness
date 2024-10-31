using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectedness.API.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupIdToQuestionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Options",
                table: "Questions",
                newName: "IncorrectOption3");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IncorrectOption1",
                table: "Questions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IncorrectOption2",
                table: "Questions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_GroupId",
                table: "Questions",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Groups_GroupId",
                table: "Questions",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Groups_GroupId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_GroupId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IncorrectOption1",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IncorrectOption2",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "IncorrectOption3",
                table: "Questions",
                newName: "Options");
        }
    }
}
