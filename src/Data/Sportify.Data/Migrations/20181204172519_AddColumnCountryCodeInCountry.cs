namespace Sportify.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddColumnCountryCodeInCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Density",
                "Countries");

            migrationBuilder.DropColumn(
                "LandArea",
                "Countries");

            migrationBuilder.DropColumn(
                "Population",
                "Countries");

            migrationBuilder.AddColumn<string>(
                "CountryCode",
                "Countries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "CountryCode",
                "Countries");

            migrationBuilder.AddColumn<int>(
                "Density",
                "Countries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "LandArea",
                "Countries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "Population",
                "Countries",
                nullable: false,
                defaultValue: 0);
        }
    }
}