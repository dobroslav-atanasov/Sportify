namespace Sportify.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddAllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                "BirthDate",
                "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                "CountryId",
                "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                "FirstName",
                "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "LastName",
                "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "PhotoUrl",
                "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                "Countries",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Countries", x => x.Id); });

            migrationBuilder.CreateTable(
                "Organizations",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    PresidentId = table.Column<int>(nullable: false),
                    PresidentId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        "FK_Organizations_AspNetUsers_PresidentId1",
                        x => x.PresidentId1,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Sports",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageSportUrl = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Sports", x => x.Id); });

            migrationBuilder.CreateTable(
                "Towns",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                    table.ForeignKey(
                        "FK_Towns_Countries_CountryId",
                        x => x.CountryId,
                        "Countries",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Disciplines",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                    table.ForeignKey(
                        "FK_Disciplines_Sports_SportId",
                        x => x.SportId,
                        "Sports",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Venues",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    ImageVenueUrl = table.Column<string>(nullable: true),
                    TownId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                    table.ForeignKey(
                        "FK_Venues_Towns_TownId",
                        x => x.TownId,
                        "Towns",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Events",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    EventName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    DisciplineId = table.Column<int>(nullable: false),
                    VenueId = table.Column<int>(nullable: false),
                    NumberOfParticipants = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        "FK_Events_Disciplines_DisciplineId",
                        x => x.DisciplineId,
                        "Disciplines",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Events_Organizations_OrganizationId",
                        x => x.OrganizationId,
                        "Organizations",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Events_Venues_VenueId",
                        x => x.VenueId,
                        "Venues",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Participants",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true),
                    UserResult = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        "FK_Participants_Events_EventId",
                        x => x.EventId,
                        "Events",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Participants_AspNetUsers_UserId1",
                        x => x.UserId1,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_AspNetUsers_CountryId",
                "AspNetUsers",
                "CountryId");

            migrationBuilder.CreateIndex(
                "IX_Disciplines_SportId",
                "Disciplines",
                "SportId");

            migrationBuilder.CreateIndex(
                "IX_Events_DisciplineId",
                "Events",
                "DisciplineId");

            migrationBuilder.CreateIndex(
                "IX_Events_OrganizationId",
                "Events",
                "OrganizationId");

            migrationBuilder.CreateIndex(
                "IX_Events_VenueId",
                "Events",
                "VenueId");

            migrationBuilder.CreateIndex(
                "IX_Organizations_PresidentId1",
                "Organizations",
                "PresidentId1");

            migrationBuilder.CreateIndex(
                "IX_Participants_EventId",
                "Participants",
                "EventId");

            migrationBuilder.CreateIndex(
                "IX_Participants_UserId1",
                "Participants",
                "UserId1");

            migrationBuilder.CreateIndex(
                "IX_Towns_CountryId",
                "Towns",
                "CountryId");

            migrationBuilder.CreateIndex(
                "IX_Venues_TownId",
                "Venues",
                "TownId");

            migrationBuilder.AddForeignKey(
                "FK_AspNetUsers_Countries_CountryId",
                "AspNetUsers",
                "CountryId",
                "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_AspNetUsers_Countries_CountryId",
                "AspNetUsers");

            migrationBuilder.DropTable(
                "Participants");

            migrationBuilder.DropTable(
                "Events");

            migrationBuilder.DropTable(
                "Disciplines");

            migrationBuilder.DropTable(
                "Organizations");

            migrationBuilder.DropTable(
                "Venues");

            migrationBuilder.DropTable(
                "Sports");

            migrationBuilder.DropTable(
                "Towns");

            migrationBuilder.DropTable(
                "Countries");

            migrationBuilder.DropIndex(
                "IX_AspNetUsers_CountryId",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "BirthDate",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "CountryId",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "FirstName",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "LastName",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "PhotoUrl",
                "AspNetUsers");
        }
    }
}