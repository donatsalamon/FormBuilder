using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formBuilder.Migrations
{
    public partial class addingFKeyToQuestionSheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormId",
                table: "QuestionSheet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSheet_FormId",
                table: "QuestionSheet",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSheet_Forms_FormId",
                table: "QuestionSheet",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSheet_Forms_FormId",
                table: "QuestionSheet");

            migrationBuilder.DropIndex(
                name: "IX_QuestionSheet_FormId",
                table: "QuestionSheet");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "QuestionSheet");
        }
    }
}
