using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace teamcmdersbackend.Migrations
{
    /// <inheritdoc />
    public partial class DailyGoalInputsCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserProgress",
                table: "Goal");

            migrationBuilder.CreateTable(
                name: "DailyGoalInputs",
                columns: table => new
                {
                    InputId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProgressInput = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GoalId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyGoalInputs", x => x.InputId);
                    table.ForeignKey(
                        name: "FK_DailyGoalInputs_Goal_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyGoalInputs_GoalId",
                table: "DailyGoalInputs",
                column: "GoalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyGoalInputs");

            migrationBuilder.AddColumn<int>(
                name: "UserProgress",
                table: "Goal",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
