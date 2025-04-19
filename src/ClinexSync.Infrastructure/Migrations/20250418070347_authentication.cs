using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClinexSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class authentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_areas", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "offices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_offices", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_districts_cities_CityId",
                        column: x => x.CityId,
                        principalTable: "cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rooms_offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    IdentityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.IdentityId);
                    table.ForeignKey(
                        name: "FK_users_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address_Street1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Street2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_PostalCode = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false
                    ),
                    Address_District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_IsBis = table.Column<bool>(type: "bit", nullable: false),
                    Address_DoorNumber = table.Column<int>(type: "int", nullable: false),
                    BirthDay = table.Column<DateOnly>(type: "date", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_persons_cities_CityId",
                        column: x => x.CityId,
                        principalTable: "cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_persons_districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "districts",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "administrators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_administrators_persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_administrators_users_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "users",
                        principalColumn: "IdentityId"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "pacients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pacients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pacients_persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_pacients_users_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "users",
                        principalColumn: "IdentityId"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "professionals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professionals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_professionals_persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_professionals_users_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "users",
                        principalColumn: "IdentityId"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "professionalAreasToWork",
                columns: table => new
                {
                    AreaToWorkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfessionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_professionalAreasToWork",
                        x => new { x.ProfessionalId, x.AreaToWorkId }
                    );
                    table.ForeignKey(
                        name: "FK_professionalAreasToWork_professionals_ProfessionalId",
                        column: x => x.ProfessionalId,
                        principalTable: "professionals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator" },
                    { 2, "Pacient" },
                    { 3, "Professional" },
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_administrators_IdentityId",
                table: "administrators",
                column: "IdentityId",
                unique: true,
                filter: "[IdentityId] IS NOT NULL"
            );

            migrationBuilder.CreateIndex(
                name: "IX_administrators_PersonId",
                table: "administrators",
                column: "PersonId",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_cities_Name",
                table: "cities",
                column: "Name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_districts_CityId",
                table: "districts",
                column: "CityId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_pacients_IdentityId",
                table: "pacients",
                column: "IdentityId",
                unique: true,
                filter: "[IdentityId] IS NOT NULL"
            );

            migrationBuilder.CreateIndex(
                name: "IX_pacients_PersonId",
                table: "pacients",
                column: "PersonId",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_persons_CityId",
                table: "persons",
                column: "CityId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_persons_DistrictId",
                table: "persons",
                column: "DistrictId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_persons_DocumentNumber",
                table: "persons",
                column: "DocumentNumber",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_persons_Email",
                table: "persons",
                column: "Email",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_persons_Phone",
                table: "persons",
                column: "Phone",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_professionals_IdentityId",
                table: "professionals",
                column: "IdentityId",
                unique: true,
                filter: "[IdentityId] IS NOT NULL"
            );

            migrationBuilder.CreateIndex(
                name: "IX_professionals_PersonId",
                table: "professionals",
                column: "PersonId",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_roles_Name",
                table: "roles",
                column: "Name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_rooms_OfficeId",
                table: "rooms",
                column: "OfficeId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_users_IdentityId",
                table: "users",
                column: "IdentityId",
                unique: true
            );

            migrationBuilder.CreateIndex(name: "IX_users_RoleId", table: "users", column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "administrators");

            migrationBuilder.DropTable(name: "areas");

            migrationBuilder.DropTable(name: "pacients");

            migrationBuilder.DropTable(name: "professionalAreasToWork");

            migrationBuilder.DropTable(name: "rooms");

            migrationBuilder.DropTable(name: "professionals");

            migrationBuilder.DropTable(name: "offices");

            migrationBuilder.DropTable(name: "persons");

            migrationBuilder.DropTable(name: "users");

            migrationBuilder.DropTable(name: "districts");

            migrationBuilder.DropTable(name: "roles");

            migrationBuilder.DropTable(name: "cities");
        }
    }
}
