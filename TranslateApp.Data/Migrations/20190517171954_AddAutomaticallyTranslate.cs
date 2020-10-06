using Microsoft.EntityFrameworkCore.Migrations;

namespace TranslateApp.Data.Migrations
{
    public partial class AddAutomaticallyTranslate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AutomaticallyTranslate",
                table: "Project",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutomaticallyTranslate",
                table: "Project");
        }
    }
}
