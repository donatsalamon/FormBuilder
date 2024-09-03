using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formBuilder.Migrations
{
    public partial class deletingFKStatementId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSheet_Statements_StatementId",
                table: "QuestionSheet");

            migrationBuilder.DropIndex(
                name: "IX_QuestionSheet_StatementId",
                table: "QuestionSheet");

            migrationBuilder.DropColumn(
                name: "StatementId",
                table: "QuestionSheet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatementId",
                table: "QuestionSheet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSheet_StatementId",
                table: "QuestionSheet",
                column: "StatementId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSheet_Statements_StatementId",
                table: "QuestionSheet",
                column: "StatementId",
                principalTable: "Statements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
