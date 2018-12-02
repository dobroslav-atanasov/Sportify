namespace Sportify.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddThreeNewColumnsInCountries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Countries");

            migrationBuilder.AddColumn<int>(
                name: "Density",
                table: "Countries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LandArea",
                table: "Countries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Population",
                table: "Countries",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Density",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "LandArea",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Population",
                table: "Countries");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Countries",
                nullable: true);
        }
    }
}
