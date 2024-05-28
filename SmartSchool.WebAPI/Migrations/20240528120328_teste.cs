using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartSchool.WebAPI.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Disciplinas",
                newName: "id");

            migrationBuilder.AddColumn<string>(
                name: "Sobrenome",
                table: "Professores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Professores",
                keyColumn: "Id",
                keyValue: 1,
                column: "Sobrenome",
                value: "TEste");

            migrationBuilder.UpdateData(
                table: "Professores",
                keyColumn: "Id",
                keyValue: 2,
                column: "Sobrenome",
                value: "TEste");

            migrationBuilder.UpdateData(
                table: "Professores",
                keyColumn: "Id",
                keyValue: 3,
                column: "Sobrenome",
                value: "TEste");

            migrationBuilder.UpdateData(
                table: "Professores",
                keyColumn: "Id",
                keyValue: 4,
                column: "Sobrenome",
                value: "TEste");

            migrationBuilder.UpdateData(
                table: "Professores",
                keyColumn: "Id",
                keyValue: 5,
                column: "Sobrenome",
                value: "TEste");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sobrenome",
                table: "Professores");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Disciplinas",
                newName: "Id");
        }
    }
}
