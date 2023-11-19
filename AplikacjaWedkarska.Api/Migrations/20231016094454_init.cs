using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AplikacjaWedkarska.Api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mountain1Active = table.Column<bool>(type: "bit", nullable: false),
                    Mountain2Active = table.Column<bool>(type: "bit", nullable: false),
                    Lowland1Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    CardID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Cards_CardID",
                        column: x => x.CardID,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "DateCreated", "DateModified", "Lowland1Active", "Mountain1Active", "Mountain2Active" },
                values: new object[,]
                {
                    { new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"), new DateTime(2023, 10, 16, 11, 44, 54, 724, DateTimeKind.Local).AddTicks(518), new DateTime(2023, 10, 16, 11, 44, 54, 724, DateTimeKind.Local).AddTicks(524), false, false, false },
                    { new Guid("3aad22ae-0e3e-4247-93bb-8b79f04123b6"), new DateTime(2023, 10, 16, 11, 44, 54, 724, DateTimeKind.Local).AddTicks(535), new DateTime(2023, 10, 16, 11, 44, 54, 724, DateTimeKind.Local).AddTicks(539), false, false, false }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "Name" },
                values: new object[,]
                {
                    { 1, "user" },
                    { 2, "controller" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CardID", "DateCreated", "DateOfBirth", "Email", "IsDeleted", "Name", "Password", "RoleID", "Surname" },
                values: new object[,]
                {
                    { new Guid("3562b6fb-8bac-4897-b542-d5cefc3fc123"), new Guid("2ef422ae-0e8e-4f47-93bb-8b79f04123b6"), new DateTime(2023, 10, 16, 11, 44, 54, 724, DateTimeKind.Local).AddTicks(608), new DateTime(2000, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "agraba@cos.nie", false, "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 2, "Graba" },
                    { new Guid("93e7f1d2-7bf9-4531-af27-57647447cb19"), new Guid("3aad22ae-0e3e-4247-93bb-8b79f04123b6"), new DateTime(2023, 10, 16, 11, 44, 54, 724, DateTimeKind.Local).AddTicks(737), new DateTime(2002, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "jdyrdul@cos.nie", false, "Jan", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Dyrduł" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CardID",
                table: "Accounts",
                column: "CardID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
