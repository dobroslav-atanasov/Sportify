namespace Sportify.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeColumnResultInParticipants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "UserResult",
                "Participants");

            migrationBuilder.AddColumn<DateTime>(
                "Result",
                "Participants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Result",
                "Participants");

            migrationBuilder.AddColumn<DateTime>(
                "UserResult",
                "Participants",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}