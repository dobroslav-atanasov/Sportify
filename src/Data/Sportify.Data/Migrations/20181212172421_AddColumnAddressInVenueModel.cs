namespace Sportify.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddColumnAddressInVenueModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Address",
                "Venues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Address",
                "Venues");
        }
    }
}