using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "species",
                columns: table => new
                {
                    specie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    specie_type = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_species", x => x.specie_id);
                });

            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    volunteer_description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    volunteer_experience = table.Column<int>(type: "integer", nullable: false),
                    volunteer_account_details_description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    volunteer_account_details_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    volunteer_email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    volunteer_phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    volunteer_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    volunteer_patronymic = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    volunteer_surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.volunteer_id);
                });

            migrationBuilder.CreateTable(
                name: "breeds",
                columns: table => new
                {
                    breed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    specie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    breed_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_breeds", x => x.breed_id);
                    table.ForeignKey(
                        name: "fk_breeds_species_specie_id",
                        column: x => x.specie_id,
                        principalTable: "species",
                        principalColumn: "specie_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    pet_id = table.Column<Guid>(type: "uuid", nullable: false),
                    specie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    breed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    pet_creation_date = table.Column<DateOnly>(type: "date", nullable: false),
                    pet_birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    pet_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    pet_description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    pet_address = table.Column<string>(type: "text", nullable: false),
                    pet_color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    pet_height = table.Column<double>(type: "double precision", nullable: false),
                    pet_weight = table.Column<double>(type: "double precision", nullable: false),
                    pet_help_status_code = table.Column<int>(type: "integer", nullable: false),
                    pet_status_help = table.Column<string>(type: "text", nullable: false),
                    owner_email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    owner_phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    pet_castrated = table.Column<bool>(type: "boolean", nullable: false),
                    pet_vaccinated = table.Column<bool>(type: "boolean", nullable: false),
                    Attachments = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pets", x => x.pet_id);
                    table.ForeignKey(
                        name: "fk_pets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "volunteer_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "social_medias",
                columns: table => new
                {
                    social_media_id = table.Column<Guid>(type: "uuid", nullable: false),
                    social_media_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    social_media_url = table.Column<string>(type: "text", nullable: false),
                    volunteer_social_media_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_social_medias", x => x.social_media_id);
                    table.ForeignKey(
                        name: "fk_social_medias_volunteers_volunteer_social_media_id",
                        column: x => x.volunteer_social_media_id,
                        principalTable: "volunteers",
                        principalColumn: "volunteer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_breeds_specie_id",
                table: "breeds",
                column: "specie_id");

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                table: "pets",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "ix_social_medias_volunteer_social_media_id",
                table: "social_medias",
                column: "volunteer_social_media_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "breeds");

            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "social_medias");

            migrationBuilder.DropTable(
                name: "species");

            migrationBuilder.DropTable(
                name: "volunteers");
        }
    }
}
