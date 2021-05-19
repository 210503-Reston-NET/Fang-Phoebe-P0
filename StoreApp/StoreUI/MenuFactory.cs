using StoreBL;
using StoreDL;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoreDL.Entities;

namespace StoreUI
{
    public class MenuFactory
    {
        public static IMenu GetMenu(string menuType) 
        {
            //configure
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            //connect
            string connectionString = configuration.GetConnectionString("StoreAppDB");
            //Console.WriteLine(connectionString);

            //option
            DbContextOptions<StoreAppDBContext> options = new DbContextOptionsBuilder<StoreAppDBContext>()
            .UseSqlServer(connectionString)
            .Options;

            //context
            var context = new StoreAppDBContext(options);

            IRepository repo = new RepoDB(context);
            IValidationService inputValidation = new ValidationService();
            ICustomerBL customerBL = new CustomerBL(repo);
            ILocationBL locationBL = new LocationBL(repo);
            IProductBL productBL = new ProductBL(repo);
            IInventoryBL inventoryBL = new InventoryBL(repo);
            IOrderBL orderBL = new OrderBL(repo);


        
            switch(menuType.ToLower())
            {
                case "main":
                    return new MainMenu();
                case "branch":
                    return new BranchMenu(locationBL, inputValidation);
                case "product":
                    return new ProductMenu(productBL, inputValidation);
                case "inventory":
                    return new InventoryMenu(locationBL, productBL, inventoryBL, inputValidation);
                 case "customer":
                    return new CustomerMenu(customerBL);
                case "order":
                    return new OrderMenu(customerBL, locationBL, productBL, inventoryBL, orderBL, inputValidation);
                default:
                    return null;
            }
        }
    }
}