using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formBuilder.Migrations
{
    public partial class addingQuestionSheetNameToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AspNetUsers_UserTableId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_QuestionSheet_QuestionId",
                table: "Answers");

            migrationBuilder.AlterColumn<string>(
                name: "UserTableId",
                table: "Answers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "Answers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "nameOfTheQuestionSheet",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AspNetUsers_UserTableId",
                table: "Answers",
                column: "UserTableId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_QuestionSheet_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "QuestionSheet",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AspNetUsers_UserTableId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_QuestionSheet_QuestionId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "nameOfTheQuestionSheet",
                table: "Answers");

            migrationBuilder.AlterColumn<string>(
                name: "UserTableId",
                table: "Answers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AspNetUsers_UserTableId",
                table: "Answers",
                column: "UserTableId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_QuestionSheet_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "QuestionSheet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
