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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mountain1Active = table.Column<bool>(type: "bit", nullable: false),
                    Mountain2Active = table.Column<bool>(type: "bit", nullable: false),
                    Lowland1Active = table.Column<bool>(type: "bit", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRegistered = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fishes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumSize = table.Column<int>(type: "int", nullable: true),
                    MaximumSize = table.Column<int>(type: "int", nullable: true),
                    ProtectionPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProtectionPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DailyLimit = table.Column<int>(type: "int", nullable: true),
                    UnableToTake = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FishingSpots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatchAndRelease = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FishingSpots", x => x.Id);
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
                    CardID = table.Column<string>(type: "nvarchar(450)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "FishingSpotLimits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DailyLimit = table.Column<int>(type: "int", nullable: true),
                    FishingSpotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FishingSpotLimits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FishingSpotLimits_Fishes_FishId",
                        column: x => x.FishId,
                        principalTable: "Fishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FishingSpotLimits_FishingSpots_FishingSpotId",
                        column: x => x.FishingSpotId,
                        principalTable: "FishingSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inspections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfInspection = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControlledUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ControllerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FishingSpotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Base64Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inspections_Accounts_ControlledUserId",
                        column: x => x.ControlledUserId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inspections_Accounts_ControllerId",
                        column: x => x.ControllerId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inspections_FishingSpots_FishingSpotId",
                        column: x => x.FishingSpotId,
                        principalTable: "FishingSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FishingSpotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_FishingSpots_FishingSpotId",
                        column: x => x.FishingSpotId,
                        principalTable: "FishingSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaughtFishes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    CatchDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaughtFishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaughtFishes_Fishes_FishId",
                        column: x => x.FishId,
                        principalTable: "Fishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaughtFishes_Reservations_ReservationEntityId",
                        column: x => x.ReservationEntityId,
                        principalTable: "Reservations",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "DateCreated", "DateModified", "Email", "IsRegistered", "Lowland1Active", "Mountain1Active", "Mountain2Active", "OwnerName", "OwnerSurname" },
                values: new object[,]
                {
                    { "000001", new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2811), new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2842), "agraba@cos.nie", true, false, false, false, "Artur", "Graba" },
                    { "000002", new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2845), new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2846), "jdyrdul@cos.nie", true, false, false, false, "Jan", "Dyrduł" },
                    { "000003", new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2849), new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2850), "kzapala@cos.nie", false, false, false, false, "Klaudia", "Zapała" },
                    { "000004", new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2852), new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2854), "ropak@cos.nie", false, false, false, false, "Remigiusz", "Opak" },
                    { "000005", new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2856), new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2857), "bsarat@cos.nie", false, false, false, false, "Benedykt", "Sarat" },
                    { "000006", new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2858), new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2860), "okataran@cos.nie", false, false, false, false, "Oliwier", "Kataran" },
                    { "000007", new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2861), new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2862), "maniol@cos.nie", false, false, false, false, "Michał", "Anioł" },
                    { "000008", new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2864), new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2865), "mlars@cos.nie", false, false, false, false, "Michał", "Lars" },
                    { "000009", new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2867), new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2868), "mjagiello@cos.nie", false, false, false, false, "Mariola", "Jagiełło" },
                    { "000010", new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2870), new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2871), "kcygan@cos.nie", false, false, false, false, "Konrad", "Cygan" }
                });

            migrationBuilder.InsertData(
                table: "Fishes",
                columns: new[] { "Id", "DailyLimit", "MaximumSize", "MinimumSize", "ProtectionPeriodEnd", "ProtectionPeriodStart", "Species", "UnableToTake" },
                values: new object[,]
                {
                    { new Guid("0391ba54-fd53-43d5-9091-dc040d82ec1a"), null, 35, 20, null, null, "Okoń", false },
                    { new Guid("0864d0d1-9b0c-45b4-955b-fec1b548f1d2"), 0, null, null, null, null, "Kiełb", false },
                    { new Guid("0af80732-4687-43f0-863f-e29811bf4ef0"), null, null, 25, null, null, "Miętus", false },
                    { new Guid("1564f4a6-4f92-4af6-b0f3-2677d91957b2"), null, null, 18, null, null, "Sielawa", false },
                    { new Guid("171265cc-b732-4e5c-8e3f-0cde27b827f9"), null, null, 15, null, null, "Płoć", false },
                    { new Guid("1834d8fa-225b-4d96-82eb-65322022ceb5"), null, null, null, null, null, "Tołpyga", false },
                    { new Guid("2a9272c5-086c-47ad-b03b-567b4044d76b"), null, 50, 30, null, null, "Lin", false },
                    { new Guid("40320d9d-3b31-4834-85dd-4c47fb5bb55f"), 2, null, null, null, null, "Amur biały", false },
                    { new Guid("57a87d78-0054-4246-be05-ad6a2b198b52"), 1, 70, 40, null, null, "Boleń", false },
                    { new Guid("580a550d-222d-4e3c-b452-5fcbe1c0f25e"), 1, 80, 55, null, null, "Sandacz", false },
                    { new Guid("625abc9e-a533-4876-a1ad-c75082739e54"), null, null, null, null, null, "Karaś chiński", false },
                    { new Guid("63ea69c8-50d5-44f2-b0de-b3fe27a9f3ca"), 2, null, 30, null, null, "Pstrąg tęczowy", false },
                    { new Guid("64fc7d5d-38ae-47e1-bb11-a635bb82ffe4"), null, null, 35, null, null, "Sieja", false },
                    { new Guid("681cc7ea-01df-4e5d-b1af-996a2f77a2fe"), 2, null, 60, null, null, "Węgorz", false },
                    { new Guid("7a3a32fa-c4c8-4368-b33d-7db55620c2e1"), null, null, null, null, null, "Karaś srebrzysty (japoniec)", false },
                    { new Guid("819ebcc7-e872-48b2-83e9-d2c72c7006f7"), null, null, null, null, null, "Ukleja", false },
                    { new Guid("885a8fa4-6d28-4c4a-b900-9c33c3394ffb"), 2, 50, 35, null, null, "Pstrąg potokowy", false },
                    { new Guid("8f2be803-ae34-4707-b255-965b172395d7"), 0, null, null, null, null, "Karaś pospolity (złocisty)", false },
                    { new Guid("9a998d2f-acbc-4fb9-871f-d60a9e64bda9"), 0, null, null, null, null, "Lipień", false },
                    { new Guid("a6eca0bb-1b43-40a5-a9c7-5a661ab23f12"), null, null, 15, null, null, "Jelec", false },
                    { new Guid("ad334f21-ca3f-6f12-bf32-12ac34ac5612"), 2, 70, 40, null, null, "Karp", false },
                    { new Guid("b1dcb9fd-bbfa-413d-a60b-b5a967592f13"), 2, null, 25, null, null, "Sapa", false },
                    { new Guid("b92cef02-7d83-4af8-b9e3-08eb860c6a2e"), 2, null, 25, null, null, "Rozpiór", false },
                    { new Guid("bc385976-3d43-447d-bb26-0ae73cf5d1c4"), 1, 60, 40, null, null, "Brzana", false },
                    { new Guid("c83cba6e-67f8-49a0-b625-3b4cb485fb27"), 3, 45, 30, null, null, "Jaź", false },
                    { new Guid("ca372d7a-6795-4778-b407-140dfb110f43"), 0, null, null, null, null, "Jesiotr", false },
                    { new Guid("ce7350d3-ddad-49f1-a36f-4f915ccd28b3"), null, null, 15, null, null, "Wzdręga", false },
                    { new Guid("cf40ea67-aa94-49fe-8e1b-745be64a4f5d"), 0, null, null, null, null, "Łosoś atlantycki", false },
                    { new Guid("d1bc95f2-69a1-41a4-8a2c-05af538d7bc8"), null, 60, null, null, null, "Leszcz", false },
                    { new Guid("d2d9be29-93e9-44a2-8bec-8c336630c7d7"), null, null, 10, null, null, "Jazgarz", false },
                    { new Guid("da442ddb-1a45-49cc-b429-a6406a2b1af0"), null, null, null, null, null, "Słonecznica", false },
                    { new Guid("dfb10add-4748-4669-96ed-31ba835c2caa"), null, null, null, null, null, "Krąp", false },
                    { new Guid("e49221b4-fcbf-4648-8264-85cc3df0205b"), 0, null, null, null, null, "Troć wędrowna", false },
                    { new Guid("ec4b479f-aa58-4fc7-9820-25910234777c"), 0, null, null, null, null, "Certa", false },
                    { new Guid("ecfb0262-4164-4d58-8de1-0838c43ffa32"), 1, null, 70, null, null, "Sum", false },
                    { new Guid("eda798c4-f5ec-4ff7-8aaf-6c17cf30e750"), 1, 90, 55, null, null, "Szczupak", false },
                    { new Guid("fc621c8f-5847-4a7a-b746-7f033de7174a"), 5, null, 25, null, null, "Świnka", false },
                    { new Guid("fdfb1a36-b8f1-42a2-bbc3-000a93ba95a5"), 3, 45, 30, null, null, "Kleń", false }
                });

            migrationBuilder.InsertData(
                table: "FishingSpots",
                columns: new[] { "Id", "Address", "CatchAndRelease", "Description", "Latitude", "Longitude", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("23e8b16c-7c2c-13a4-557d-7d1aa32d4a23"), "Jaśle - Stawik, 26-140 Łączna ", false, "Jest to niewielki zbiornik na rzeczce w miejscowości Jaśle. Żyją w nim ładne ryby. Cisza nad wodą dopełnia wędkarskiego szczęście. Głębokość akwenu wynosi od około 1 m przy wpływie rzeczki do ponad 4 m przy wypływie. W połowie zbiornika znajduje się betonowy pomost, który można wykorzystać do celów wędkarskich. W jego pobliżu zawsze kręcą się drapieżniki. Zbiornik ma dopiero kilka lat, mimo to można spotkać tu różne gatunki ryb. Japońce, karpie, liny, płocie, jazie gwarantują zajęcie dla wędkarzy z lekką gruntówką bądź spławikówką. Zestaw ze spławikiem można usytuować zaraz za trzcinami. Ryby przebywają tam chętnie z powodu dużej ilości pokarmu. Większość białorybu bardzo dobrze bierze na kukurydzę. Aby zwiększyć swoje szanse, niektórzy wędkarze nęcą łowisko kilka dni przed planowaną wyprawą. Z drapieżników możemy się spodziewać szczupaków i okoni. Szczupaki lubią duże obrotówki bądź wahadłówki. Białym twisterem też można dobrze połowić. Na okonie nie wybieram się bez motor oil. Większe osobniki bywają kapryśne. Jednego dnia biorą dobrze, a następnego nie reagują na nic. ", 51.001476608965859, 20.775563090091644, "Zbiornik Jaśle", "Nizinna" },
                    { new Guid("29aa3ffd-02fe-4f47-93bb-b4d9f041654f"), "Dawidowicza 3, 26-130 Suchedniów", true, "Zalew o powierzchni lustra wody bliskiej 22 ha położony na rzece Kamionce. Zbiornik znajduje się w centrum miasta sąsiadując z parkiem miejskim. Charakterystyczną cechą tego zbiornika jest wyspa położona na środku zbiornika stanowiąca rezerwat ptactwa wodnego i chronionych gatunków zwierząt. Głębokość zbiornika jest niewielka, a dno jest w większości muliste. Przy zalewie działa Ośrodek Sportu i Rekreacji (OSiR), gdzie jest do dyspozycji baza noclegowa, kąpielisko z kilkunastometrową plażą, kort tenisowy, boisko do gry w siatkę plażową oraz wiele innych atrakcji. Zalew jest łowny z każdego miejsca, a wędkarze nie narzekają na efekty. Zbiornik słynie z dużych okazów karpi, amurów, sandaczy. Jest również dużo leszcza, lecz w większości skąpych rozmiarów, ale trafiają się i okazy w granicach 60 cm. Złowimy tu również obie odmiany karasia, płocie, wzdręgi, jazie, liny, okonie i sandacze. ", 51.048285319876271, 20.843287684659408, "Zalew w Suchedniowie", "Nizinna" }
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
                    { new Guid("11aab16c-7c2c-13a4-557d-7d1aa32d4a23"), "000001", new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2908), new DateTime(2000, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "agraba@cos.nie", false, "Artur", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 2, "Graba" },
                    { new Guid("22bbb16c-7c2c-13a4-557d-7d1aa32d4a23"), "000002", new DateTime(2023, 12, 3, 10, 39, 48, 170, DateTimeKind.Local).AddTicks(2985), new DateTime(2000, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "jdyrdul@cos.nie", false, "Jan", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 1, "Dyrduł" }
                });

            migrationBuilder.InsertData(
                table: "FishingSpotLimits",
                columns: new[] { "Id", "DailyLimit", "FishId", "FishingSpotId" },
                values: new object[] { new Guid("5975fe07-8041-4454-b3e6-29e70196ad41"), 1, new Guid("ad334f21-ca3f-6f12-bf32-12ac34ac5612"), new Guid("23e8b16c-7c2c-13a4-557d-7d1aa32d4a23") });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "AccountId", "DateCreated", "FishingSpotId", "IsActive", "ReservationStart" },
                values: new object[,]
                {
                    { new Guid("55e8b16c-7c2c-13a4-557d-7d1aa32d4a23"), new Guid("11aab16c-7c2c-13a4-557d-7d1aa32d4a23"), new DateTime(2023, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("23e8b16c-7c2c-13a4-557d-7d1aa32d4a23"), false, new DateTime(2023, 11, 5, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("66aa3ffd-02fe-4f47-93bb-b4d9f041654f"), new Guid("22bbb16c-7c2c-13a4-557d-7d1aa32d4a23"), new DateTime(2023, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("29aa3ffd-02fe-4f47-93bb-b4d9f041654f"), false, new DateTime(2023, 11, 7, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("99e8b16c-7c2c-13a4-557d-7d1aa32d4a23"), new Guid("11aab16c-7c2c-13a4-557d-7d1aa32d4a23"), new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("23e8b16c-7c2c-13a4-557d-7d1aa32d4a23"), true, new DateTime(2023, 12, 3, 10, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CardID",
                table: "Accounts",
                column: "CardID");

            migrationBuilder.CreateIndex(
                name: "IX_CaughtFishes_FishId",
                table: "CaughtFishes",
                column: "FishId");

            migrationBuilder.CreateIndex(
                name: "IX_CaughtFishes_ReservationEntityId",
                table: "CaughtFishes",
                column: "ReservationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_FishingSpotLimits_FishId",
                table: "FishingSpotLimits",
                column: "FishId");

            migrationBuilder.CreateIndex(
                name: "IX_FishingSpotLimits_FishingSpotId",
                table: "FishingSpotLimits",
                column: "FishingSpotId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_ControlledUserId",
                table: "Inspections",
                column: "ControlledUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_ControllerId",
                table: "Inspections",
                column: "ControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_FishingSpotId",
                table: "Inspections",
                column: "FishingSpotId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AccountId",
                table: "Reservations",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FishingSpotId",
                table: "Reservations",
                column: "FishingSpotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaughtFishes");

            migrationBuilder.DropTable(
                name: "FishingSpotLimits");

            migrationBuilder.DropTable(
                name: "Inspections");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Fishes");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "FishingSpots");

            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
