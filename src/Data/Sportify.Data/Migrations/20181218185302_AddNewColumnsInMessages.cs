namespace Sportify.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddNewColumnsInMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Name",
                "Messages",
                "Subject");

            migrationBuilder.AddColumn<string>(
                "Email",
                "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "FullName",
                "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Email",
                "Messages");

            migrationBuilder.DropColumn(
                "FullName",
                "Messages");

            migrationBuilder.RenameColumn(
                "Subject",
                "Messages",
                "Name");
        }
    }
}