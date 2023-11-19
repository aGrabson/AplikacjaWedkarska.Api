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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                    Id = Guid.Parse("2EF422AE-0E8E-4F47-93BB-8B79F04123B6"),
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Mountain1Active = false,
                    Mountain2Active = false,
                    Lowland1Active = false,
                },
                new CardEntity
                {
                    Id = Guid.Parse("3AAD22AE-0E3E-4247-93BB-8B79F04123B6"),
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Mountain1Active = false,
                    Mountain2Active = false,
                    Lowland1Active = false,
                }
            );

            modelBuilder.Entity<AccountEntity>().HasData(
                new AccountEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Artur",
                    Surname = "Graba",
                    Email = "agraba@cos.nie",
                    Password = password,
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.ParseExact("26-11-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    IsDeleted = false,
                    RoleID = 2,
                    CardID = Guid.Parse("2EF422AE-0E8E-4F47-93BB-8B79F04123B6"),
                },
                new AccountEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Jan",
                    Surname = "Dyrduł",
                    Email = "jdyrdul@cos.nie",
                    Password = password,
                    DateCreated = DateTime.Now,
                    DateOfBirth = DateTime.ParseExact("04-11-2002", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    IsDeleted = false,
                    RoleID = 1,
                    CardID = Guid.Parse("3AAD22AE-0E3E-4247-93BB-8B79F04123B6"),
                }
            );
            
        }
    }
}
