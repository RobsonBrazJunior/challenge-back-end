using Microsoft.EntityFrameworkCore.Migrations;

namespace ChallengeBackEnd.Data.Migrations
{
    public partial class CriacaoPropriedadeResumo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resumo",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resumo",
                table: "Posts");
        }
    }
}
