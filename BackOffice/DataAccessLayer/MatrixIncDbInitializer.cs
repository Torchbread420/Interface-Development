using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class MatrixIncDbInitializer
    {
        public static void Initialize(MatrixIncDbContext context)
        {
            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            // TODO: Hier moet ik nog wat namen verzinnen die betrekking hebben op de matrix.
            // - Denk aan de m3 boutjes, moertjes en ringetjes.
            // - Denk aan namen van schepen
            // - Denk aan namen van vliegtuigen            
            var users = new User[]
            {
                new User { Name = "Neo", Password = "password1", Email = "neo@matrix.com", Address = "123 Elm St", DateOfBirth = DateOnly.Parse("1995-03-12"), PhoneNumber = 1234567890, UserType = null },
                new User { Name = "Morpheus", Password = "password2", Email = "morpheus@matrix.com", Address = "456 Oak St", DateOfBirth = DateOnly.Parse("1990-07-20"), PhoneNumber = 0987654321, UserType = null },
                new User { Name = "Trinity", Password = "password3", Email = "trinity@matrix.com", Address = "789 Pine St", DateOfBirth = DateOnly.Parse("1992-11-30"), PhoneNumber = 01234957693, UserType = null }
            };
            context.Users.AddRange(users);

            var orders = new Order[]
            {
                new Order { User = users[0], OrderDate = DateTime.Parse("2021-01-01")},
                new Order { User = users[0], OrderDate = DateTime.Parse("2021-02-01")},
                new Order { User = users[1], OrderDate = DateTime.Parse("2021-02-01")},
                new Order { User = users[2], OrderDate = DateTime.Parse("2021-03-01")}
            };  
            context.Orders.AddRange(orders);

            var products = new Product[]
            {
                new Product { Name = "Nebuchadnezzar", Id = 1, ImagePath = "./Test1", Availability = 500, MinimumAvailablility = 5, KostPrice = 8000.00m, SalePercentage = 0, Category = "Ship", Status = "Op voorraad", Location = "B2" , SKU = "N-1", Description = "Het schip waarop Neo voor het eerst de echte wereld leert kennen", Price = 10000.00m },
                new Product { Name = "Jack-in Chair", Id = 2, ImagePath = "./Test2",  Availability = 400, MinimumAvailablility = 5, KostPrice = 300.00m, SalePercentage = 0, Category = "Chair", Status = "Niet op voorraad", Location = "C2", SKU = "JC-2", Description = "Stoel met een rugsteun en metalen armen waarin mensen zitten om ingeplugd te worden in de Matrix via een kabel in de nekpoort", Price = 500.50m },
                new Product { Name = "EMP (Electro-Magnetic Pul se) Device", Id = 3, ImagePath = "./Test3",Availability = 300, MinimumAvailablility = 5, KostPrice = 100.00m, SalePercentage = 0, Status = "Laag voorraad", Category = "Weapon", Location = "D2", SKU = "E(PD-3)", Description = "Wapentuig op de schepen van Zion", Price = 129.99m }
            };
            context.Products.AddRange(products);

            var parts = new Part[]
            {
                new Part { Name = "Tandwiel", Description = "Overdracht van rotatie in bijvoorbeeld de motor of luikmechanismen"},
                new Part { Name = "M5 Boutje", Description = "Bevestiging van panelen, buizen of interne modules"},
                new Part { Name = "Hydraulische cilinder", Description = "Openen/sluiten van zware luchtsluizen of bewegende onderdelen"},
                new Part { Name = "Koelvloeistofpomp", Description = "Koeling van de motor of elektronische systemen."}
            };
            context.Parts.AddRange(parts);

            context.SaveChanges();

            context.Database.EnsureCreated();
        }
    }
}
