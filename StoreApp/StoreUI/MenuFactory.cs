using StoreBL;
using StoreDL;
using System;
namespace StoreUI
{
    public class MenuFactory
    {
        public static IMenu GetMenu(string menuType) 
        {
            switch(menuType.ToLower())
            {
                case "main":
                    return new MainMenu();
                case "store":
                Console.WriteLine("calling store menu");
                    return new StoreMenu(new LocationBL(new RepoSC()));
                case "inventory":
                    return new InventoryMenu();
                 case "customer":
                    return new CustomerMenu();
                case "order":
                    return new OrderMenu();
                default:
                    return null;
            }
        }
    }
}