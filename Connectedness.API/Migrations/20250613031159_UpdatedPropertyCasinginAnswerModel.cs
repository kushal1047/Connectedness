using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectedness.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPropertyCasinginAnswerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isCorrect",
                table: "Answers",
                newName: "IsCorrect");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCorrect",
                table: "Answers",
                newName: "isCorrect");
        }
    }
}
