using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDL;
using Entity = StoreDL.Entities;
using Model = StoreModels;

namespace StoreAppTests
{
    public class RepoTest
    {
        private readonly DbContextOptions<Entity.StoreAppDBContext> options;
        public RepoTest()
        {
            options = new DbContextOptionsBuilder<Entity.StoreAppDBContext>().UseSqlite("Filename=Test.db").Options;
            Seed();
        }
        [Fact]
        public void AddCustomerShoudAddCustomer()
        {
            using (var context = new Entity.StoreAppDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                _repo.AddCustomer(
                    new Model.Customer("Jia", "W", "Fang", "2063317069")
                );
            }
            using (var assertContext = new Entity.StoreAppDBContext(options))
            {
                //IRepository _repo = new RepoDB(assertContext);
                var result = assertContext.Customers.FirstOrDefault(c => c.Id == 2);
                Assert.NotNull(result);
                Assert.Equal("Jia", result.FirstName);
            }
        }
        
        [Fact]
        public void GetCustomerByNameShouldReturnCorrespondingCustomer()
        {
            using (var context = new Entity.StoreAppDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                var result = _repo.GetCustomer("2063317069");
                Assert.Equal("Fang", result.LasttName);
            }
        }

        [Fact]
        public void UpdateInventoryByProductId()
        {
            using (var context = new Entity.StoreAppDBContext(options))
            {
                IRepository _repo = new RepoDB(context);
                Model.Product product = _repo.GetProductById(1);
            
                _repo.UpdateInventory(product, 10);
                var result = _repo.GetInventory(product);

                Assert.Equal(10, result.Quantity);
            }
        }
        
        public void Seed()
        {
            using (var context = new Entity.StoreAppDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Customers.AddRange
                (
                    new Entity.Customer
                    {
                        Id = 1,
                        LastName = "Yang",
                        MiddleName = "",
                        FirstName = "Jessie",
                        PhoneNumber = "2067798888"
                    },
                    new Entity.Customer
                    {
                        Id = 2,
                        LastName = "Fang",
                        MiddleName = "W",
                        FirstName = "Jia",
                        PhoneNumber = "2063317069"
                    }
                );

                context.Products.AddRange(
                    new Entity.Product
                    {
                        Id = 1,
                        Name = "Jasmine Green Tea w/ Salted Cheese",
                        Price = 4.50m,
                        Barcode = "j-g-t-s-01"
                    },
                    new Entity.Product
                    {
                        Id = 2,
                        Name = "Classic Milk Tea",
                        Price = 3.99m,
                        Barcode = "c-m-t-01"
                    }
                );

                context.Locations.AddRange(
                    new Entity.Location
                    {
                        Id = 1,
                        Name = "WA-Seattle",
                        Address = "2245 8th Ave., R1-1A Seattle WA 98121"
                    }
                );

                context.Inventories.AddRange(
                    new Entity.Inventory
                    {
                        Id = 1,
                        LocationId = 1,
                        ProductId = 1,
                        Quantity = 2
                    }
                );


                context.SaveChanges();
            }
        }
    }
}