using AplikacjaWedkarska.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace AplikacjaWedkarska.Api.Data
{ 

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<CardEntity> Cards { get; set; }
        public DbSet<FishEntity> Fishes { get; set; }
        public DbSet<FishingSpotEntity> FishingSpots { get; set; }
        public DbSet<FishingSpotLimitEntity> FishingSpotLimits { get; set; }
        public DbSet<InspectionEntity> Inspections { get; set; }
        public DbSet<ReservationEntity> Reservations { get; set; }
        public DbSet<CaughtFishEntity> CaughtFishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InspectionEntity>()
                .HasOne(f => f.Controller)
                .WithMany()
                .HasForeignKey(f => f.ControllerId).OnDelete(DeleteBehavior.NoAction);


            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes("admin");
            byte[] hash = sha256.ComputeHash(bytes);
            string password = Convert.ToBase64String(hash);

            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity
                {
                    RoleID = 1,
                    Name = "user",
                },
                new RoleEntity
                {
                    RoleID = 2,
                    Name = "controller",
                }
            );
            modelBuilder.Entity<CardEntity>().HasData(
                    new CardEntity
                    {
                        Id = "000001",
                        DateModified = DateTime.Now,
                        Mountain1Active = false,
                        Mountain2Active = false,
                        Lowland1Active = false,
                        OwnerName = "Artur",
                        OwnerSurname = "Graba",
                        Email = "agraba@cos.nie",
                        IsRegistered = true,
                    },
                    new CardEntity
                    {
                        Id = "000002",
                        DateModified = DateTime.Now,
                        Mountain1Active = false,
                        Mountain2Active = false,
                        Lowland1Active = false,
                        OwnerName = "Jan",
                        OwnerSurname = "Dyrduł",
                        Email = "jdyrdul@cos.nie",
                        IsRegistered = true,
                    },
                    new CardEntity
                    {
                        Id = "000003",
                        DateModified = DateTime.Now,
                        Mountain1Active = false,
                        Mountain2Active = false,
                        Lowland1Active = false,
                        OwnerName = "Klaudia",
                        OwnerSurname = "Zapała",
                        Email = "kzapala@cos.nie",
                        IsRegistered = false,
                    },
                    new CardEntity
                    {
                        Id = "000004",
                        DateModified = DateTime.Now,
                        Mountain1Active = false,
                        Mountain2Active = false,
                        Lowland1Active = false,
                        OwnerName = "Remigiusz",
                        OwnerSurname = "Opak",
                        Email = "ropak@cos.nie",
                        IsRegistered = false,
                    },
                    new CardEntity
                    {
                        Id = "000005",
                        DateModified = DateTime.Now,
                        Mountain1Active = false,
                        Mountain2Active = false,
                        Lowland1Active = false,
                        OwnerName = "Benedykt",
                        OwnerSurname = "Sarat",
                        Email = "bsarat@cos.nie",
                        IsRegistered = false,
                    },
                    new CardEntity
                    {
                        Id = "000006",
                        DateModified = DateTime.Now,
                        Mountain1Active = false,
                        Mountain2Active = false,
                        Lowland1Active = false,
                        OwnerName = "Oliwier",
                        OwnerSurname = "Kataran",
                        Email = "okataran@cos.nie",
                        IsRegistered = false,
                    },
                    new CardEntity
                    {
                        Id = "000007",
                        DateModified = DateTime.Now,
                        Mountain1Active = false,
                        Mountain2Active = false,
                        Lowland1Active = false,
                        OwnerName = "Michał",
                        OwnerSurname = "Anioł",
                        Email = "maniol@cos.nie",
                        IsRegistered = false,
                    },
                    new CardEntity
                    {
                        Id = "000008",
                        DateModified = DateTime.Now,
                        Mountain1Active = false,
                        Mountain2Active = false,
                        Lowland1Active = false,
                        OwnerName = "Michał",
                        OwnerSurname = "Lars",
                        Email = "mlars@cos.nie",
                        IsRegistered = false,
                    },
                    new CardEntity
                    {
                        Id = "000009",
                        DateModified = DateTime.Now,
                        Mountain1Active = false,
                        Mountain2Active = false,
                        Lowland1Active = false,
                        OwnerName = "Mariola",
                        OwnerSurname = "Jagiełło",
                        Email = "mjagiello@cos.nie",
                        IsRegistered = false,
                    },
                    new CardEntity
                    {
                        Id = "000010",
                        DateModified = DateTime.Now,
                        Mountain1Active = false,
                        Mountain2Active = false,
                        Lowland1Active = false,
                        OwnerName = "Konrad",
                        OwnerSurname = "Cygan",
                        Email = "kcygan@cos.nie",
                        IsRegistered = false,
                    }
                );

            modelBuilder.Entity<AccountEntity>().HasData(
                new AccountEntity
                {
                    Id = Guid.Parse("11AAB16C-7C2C-13A4-557D-7D1AA32D4A23"),
                    Name = "Artur",
                    Surname = "Graba",
                    Email = "agraba@cos.nie",
                    Password = password,
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.ParseExact("26-11-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    IsDeleted = false,
                    RoleID = 2,
                    CardID = "000001",
                },
                new AccountEntity
                {
                    Id = Guid.Parse("22BBB16C-7C2C-13A4-557D-7D1AA32D4A23"),
                    Name = "Jan",
                    Surname = "Dyrduł",
                    Email = "jdyrdul@cos.nie",
                    Password = password,
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.ParseExact("04-11-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    IsDeleted = false,
                    RoleID = 1,
                    CardID = "000002",
                }
                );

            modelBuilder.Entity<FishEntity>().HasData(
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Amur biały",
                    MinimumSize = null,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 2
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Boleń",
                    MinimumSize = 40,
                    MaximumSize = 70,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 1
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Brzana",
                    MinimumSize = 40,
                    MaximumSize = 60,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 1
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Certa",
                    MinimumSize = null,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 0
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Jaź",
                    MinimumSize = 30,
                    MaximumSize = 45,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 3
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Jazgarz",
                    MinimumSize = 10,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Jelec",
                    MinimumSize = 15,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.Parse("AD334F21-CA3F-6F12-BF32-12AC34AC5612"),
                    Species = "Karp",
                    MinimumSize = 40,
                    MaximumSize = 70,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 2
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Karaś chiński",
                    MinimumSize = null,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Karaś pospolity (złocisty)",
                    MinimumSize = null,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 0
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Karaś srebrzysty (japoniec)",
                    MinimumSize = null,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Kiełb",
                    MinimumSize = null,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 0
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Kleń",
                    MinimumSize = 30,
                    MaximumSize = 45,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 3
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Krąp",
                    MinimumSize = null,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Leszcz",
                    MinimumSize = null,
                    MaximumSize = 60,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Lin",
                    MinimumSize = 30,
                    MaximumSize = 50,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Lipień",
                    MinimumSize = null,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 0
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Łosoś atlantycki",
                    MinimumSize = null,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 0
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Miętus",
                    MinimumSize = 25,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Okoń",
                    MinimumSize = 20,
                    MaximumSize = 35,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Płoć",
                    MinimumSize = 15,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Pstrąg potokowy",
                    MinimumSize = 35,
                    MaximumSize = 50,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 2
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Pstrąg tęczowy",
                    MinimumSize = 30,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 2
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Rozpiór",
                    MinimumSize = 25,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 2
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Sandacz",
                    MinimumSize = 55,
                    MaximumSize = 80,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 1
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Sapa",
                    MinimumSize = 25,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 2
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Sieja",
                    MinimumSize = 35,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Sielawa",
                    MinimumSize = 18,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Słonecznica",
                    MinimumSize = null,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Sum",
                    MinimumSize = 70,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 1
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Szczupak",
                    MinimumSize = 55,
                    MaximumSize = 90,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 1
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Świnka",
                    MinimumSize = 25,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 5
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Tołpyga",
                    MinimumSize = null,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Troć wędrowna",
                    MinimumSize = null,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 0
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Ukleja",
                    MinimumSize = null,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Węgorz",
                    MinimumSize = 60,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 2
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Wzdręga",
                    MinimumSize = 15,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = null
                },
                new FishEntity
                {
                    Id = Guid.NewGuid(),
                    Species = "Jesiotr",
                    MinimumSize = null,
                    MaximumSize = null,
                    ProtectionPeriodStart = null,
                    ProtectionPeriodEnd = null,
                    DailyLimit = 0
                }
            );

            modelBuilder.Entity<FishingSpotEntity>().HasData(
                new FishingSpotEntity
                {
                    Id = Guid.Parse("23E8B16C-7C2C-13A4-557D-7D1AA32D4A23"),
                    Title = "Zbiornik Jaśle",
                    Address = "Jaśle - Stawik, 26-140 Łączna ",
                    Description = "Jest to niewielki zbiornik na rzeczce w miejscowości Jaśle. Żyją w nim ładne ryby. Cisza nad wodą dopełnia wędkarskiego szczęście. Głębokość akwenu wynosi od około 1 m przy wpływie rzeczki do ponad 4 m przy wypływie. W połowie zbiornika znajduje się betonowy pomost, który można wykorzystać do celów wędkarskich. W jego pobliżu zawsze kręcą się drapieżniki. Zbiornik ma dopiero kilka lat, mimo to można spotkać tu różne gatunki ryb. Japońce, karpie, liny, płocie, jazie gwarantują zajęcie dla wędkarzy z lekką gruntówką bądź spławikówką. Zestaw ze spławikiem można usytuować zaraz za trzcinami. Ryby przebywają tam chętnie z powodu dużej ilości pokarmu. Większość białorybu bardzo dobrze bierze na kukurydzę. Aby zwiększyć swoje szanse, niektórzy wędkarze nęcą łowisko kilka dni przed planowaną wyprawą. Z drapieżników możemy się spodziewać szczupaków i okoni. Szczupaki lubią duże obrotówki bądź wahadłówki. Białym twisterem też można dobrze połowić. Na okonie nie wybieram się bez motor oil. Większe osobniki bywają kapryśne. Jednego dnia biorą dobrze, a następnego nie reagują na nic. ",
                    Latitude = 51.00147660896586,
                    Longitude = 20.775563090091644,
                    Type = "Nizinna",
                    CatchAndRelease = false
                },
                new FishingSpotEntity
                {
                    Id = Guid.Parse("29AA3FFD-02FE-4F47-93BB-B4D9F041654F"),
                    Title = "Zalew w Suchedniowie",
                    Address = "Dawidowicza 3, 26-130 Suchedniów",
                    Description = "Zalew o powierzchni lustra wody bliskiej 22 ha położony na rzece Kamionce. Zbiornik znajduje się w centrum miasta sąsiadując z parkiem miejskim. Charakterystyczną cechą tego zbiornika jest wyspa położona na środku zbiornika stanowiąca rezerwat ptactwa wodnego i chronionych gatunków zwierząt. Głębokość zbiornika jest niewielka, a dno jest w większości muliste. Przy zalewie działa Ośrodek Sportu i Rekreacji (OSiR), gdzie jest do dyspozycji baza noclegowa, kąpielisko z kilkunastometrową plażą, kort tenisowy, boisko do gry w siatkę plażową oraz wiele innych atrakcji. Zalew jest łowny z każdego miejsca, a wędkarze nie narzekają na efekty. Zbiornik słynie z dużych okazów karpi, amurów, sandaczy. Jest również dużo leszcza, lecz w większości skąpych rozmiarów, ale trafiają się i okazy w granicach 60 cm. Złowimy tu również obie odmiany karasia, płocie, wzdręgi, jazie, liny, okonie i sandacze. ",
                    Latitude = 51.04828531987627,
                    Longitude = 20.84328768465941,
                    Type = "Nizinna",
                    CatchAndRelease = true
                }
            );
            modelBuilder.Entity<ReservationEntity>().HasData(
               new ReservationEntity
               {
                   Id = Guid.Parse("55E8B16C-7C2C-13A4-557D-7D1AA32D4A23"),
                   DateCreated = DateTime.ParseExact("04-11-2023", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                   ReservationStart = DateTime.ParseExact("05-11-2023 15:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                   IsActive = false,
                   AccountId = Guid.Parse("11AAB16C-7C2C-13A4-557D-7D1AA32D4A23"),
                   FishingSpotId= Guid.Parse("23E8B16C-7C2C-13A4-557D-7D1AA32D4A23"),
                   CaughtFishes = null,
               },
               new ReservationEntity
               {
                   Id = Guid.Parse("99E8B16C-7C2C-13A4-557D-7D1AA32D4A23"),
                   DateCreated = DateTime.ParseExact("01-12-2023", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                   ReservationStart = DateTime.ParseExact("03-12-2023 10:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                   IsActive = true,
                   AccountId = Guid.Parse("11AAB16C-7C2C-13A4-557D-7D1AA32D4A23"),
                   FishingSpotId = Guid.Parse("23E8B16C-7C2C-13A4-557D-7D1AA32D4A23"),
                   CaughtFishes = null,
               },
               new ReservationEntity
               {
                   Id = Guid.Parse("66AA3FFD-02FE-4F47-93BB-B4D9F041654F"),
                   DateCreated = DateTime.ParseExact("05-11-2023", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                   ReservationStart = DateTime.ParseExact("07-11-2023 10:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                   IsActive = false,
                   AccountId = Guid.Parse("22BBB16C-7C2C-13A4-557D-7D1AA32D4A23"),
                   FishingSpotId = Guid.Parse("29AA3FFD-02FE-4F47-93BB-B4D9F041654F"),
                   CaughtFishes = null,
               }
           );


        modelBuilder.Entity<FishingSpotLimitEntity>().HasData(
                new FishingSpotLimitEntity
                {
                    Id = Guid.NewGuid(),
                    FishId = Guid.Parse("AD334F21-CA3F-6F12-BF32-12AC34AC5612"),
                    DailyLimit = 1,
                    FishingSpotId = Guid.Parse("23E8B16C-7C2C-13A4-557D-7D1AA32D4A23")
                }
            );
        }
    }
}
