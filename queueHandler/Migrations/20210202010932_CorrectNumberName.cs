using Microsoft.EntityFrameworkCore.Migrations;

namespace queuehandler.Migrations
{
    public partial class CorrectNumberName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numbero",
                table: "Ticket",
                newName: "Numero");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Ticket",
                newName: "Numbero");
        }
    }
}
