using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formBuilder.Migrations
{
    public partial class addingScoreToAnswerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Answers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Answers");
        }
    }
}
