using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace formBuilder.Migrations
{
    public partial class addingStatementToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatementId",
                table: "QuestionSheet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Statements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statements", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSheet_Statements_StatementId",
                table: "QuestionSheet");

            migrationBuilder.DropTable(
                name: "Statements");

            migrationBuilder.DropIndex(
                name: "IX_QuestionSheet_StatementId",
                table: "QuestionSheet");

            migrationBuilder.DropColumn(
                name: "StatementId",
                table: "QuestionSheet");
        }
    }
}
