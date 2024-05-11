using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace teamcmdersbackend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserCurrent",
                table: "Goal",
                newName: "UserProgress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserProgress",
                table: "Goal",
                newName: "UserCurrent");
        }
    }
}
