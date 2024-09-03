using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formBuilder.Migrations
{
    public partial class addingQuestionTypeIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isParent",
                table: "QuestionSheet");

            migrationBuilder.AddColumn<int>(
                name: "parentQuestionId",
                table: "QuestionSheet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "typeOfQuestion",
                table: "QuestionSheet",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "parentQuestionId",
                table: "QuestionSheet");

            migrationBuilder.DropColumn(
                name: "typeOfQuestion",
                table: "QuestionSheet");

            migrationBuilder.AddColumn<bool>(
                name: "isParent",
                table: "QuestionSheet",
                type: "bit",
                nullable: true);
        }
    }
}
