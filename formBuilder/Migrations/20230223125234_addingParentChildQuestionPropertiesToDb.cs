using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formBuilder.Migrations
{
    public partial class addingParentChildQuestionPropertiesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "childQuestionId",
                table: "QuestionSheet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isParent",
                table: "QuestionSheet",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "childQuestionId",
                table: "QuestionSheet");

            migrationBuilder.DropColumn(
                name: "isParent",
                table: "QuestionSheet");
        }
    }
}
