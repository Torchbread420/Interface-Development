using BackOffice.Models;
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
        
            var users = new User[]
            {
                new User { Name = "Neo", Password = "password1", Email = "neo@matrix.com", Address = "123 Elm St", DateOfBirth = DateOnly.Parse("1995-03-12"), PhoneNumber = 1234567890, UserType = null },
                new User { Name = "Morpheus", Password = "password2", Email = "morpheus@matrix.com", Address = "456 Oak St", DateOfBirth = DateOnly.Parse("1990-07-20"), PhoneNumber = 0987654321, UserType = null },
                new User { Name = "Trinity", Password = "password3", Email = "trinity@matrix.com", Address = "789 Pine St", DateOfBirth = DateOnly.Parse("1992-11-30"), PhoneNumber = 01234957693, UserType = null }
            };
            context.Users.AddRange(users);

            var products = new Product[]
            {
                new Product { Name = "Nebuchadnezzar", Id = 1, ImagePath = "./Test1", Availability = 500, MinimumAvailablility = 50, KostPrice = 8000.00m, SalePercentage = 0, Category = "Ship", Status = "Op voorraad", Location = "B2" , SKU = "N-1", Description = "Het schip waarop Neo voor het eerst de echte wereld leert kennen", Price = 10000.00m },
                new Product { Name = "Jack-in Chair", Id = 2, ImagePath = "./Test2",  Availability = 0, MinimumAvailablility = 50, KostPrice = 300.00m, SalePercentage = 0, Category = "Chair", Status = "Niet op voorraad", Location = "C2", SKU = "JC-2", Description = "Stoel met een rugsteun en metalen armen waarin mensen zitten om ingeplugd te worden in de Matrix via een kabel in de nekpoort", Price = 500.50m },
                new Product { Name = "EMP (Electro-Magnetic Pul se) Device", Id = 3, ImagePath = "./Test3",Availability = 300, MinimumAvailablility = 500, KostPrice = 100.00m, SalePercentage = 0, Status = "Laag voorraad", Category = "Weapon", Location = "D2", SKU = "E(PD-3)", Description = "Wapentuig op de schepen van Zion", Price = 129.99m }
            };
            context.Products.AddRange(products);

            var orders = new Order[]
{
                new Order { Name = "Moer V6 3/4 Bestelling", OrderTime = TimeOnly.Parse("09:15"), OrderDate = DateOnly.Parse("2025-05-12"), OrderStatus = "Voltooid", User = users[0], OrderProducts = new List<OrderProduct> { new OrderProduct { Product = products[0], Quantity = 2 }, new OrderProduct { Product = products[1], Quantity = 1 } } },
                new Order { Name = "Tandwiel Set Bestelling", OrderTime = TimeOnly.Parse("11:30"), OrderDate = DateOnly.Parse("2025-05-13"), OrderStatus = "In behandeling", User = users[1], OrderProducts = new List<OrderProduct> { new OrderProduct { Product = products[1], Quantity = 1 }, new OrderProduct { Product = products[2], Quantity = 1 }, new OrderProduct { Product = products[0], Quantity = 1 } } },
                new Order { Name = "Veerring Assortiment", OrderTime = TimeOnly.Parse("14:45"), OrderDate = DateOnly.Parse("2025-05-14"), OrderStatus = "Verzonden", User = users[0], OrderProducts = new List<OrderProduct> { new OrderProduct { Product = products[2], Quantity = 3 } } }
};
            context.Orders.AddRange(orders);

            context.SaveChanges();

            context.Database.EnsureCreated();
        }
    }
}
