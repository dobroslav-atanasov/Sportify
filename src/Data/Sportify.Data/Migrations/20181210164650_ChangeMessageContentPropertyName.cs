namespace Sportify.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeMessageContentPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "MessageContent",
                "Messages",
                "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Content",
                "Messages",
                "MessageContent");
        }
    }
}