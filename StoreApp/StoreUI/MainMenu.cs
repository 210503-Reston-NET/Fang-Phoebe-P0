using System;

namespace StoreUI
{
    public class MainMenu : IMenu
    {
        private IMenu submenu;
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Welcome to the Store App");
                Console.WriteLine("Manage:");
                Console.WriteLine("[1] Orders"); 
                Console.WriteLine("[2] Stores");
                Console.WriteLine("[3] Inventories"); 
                Console.WriteLine("[4] Customers"); 
                Console.WriteLine("[0] Exit the program");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0" :
                        Console.WriteLine("Thanks for using the system. Goodbye");
                        repeat = false;
                        break;                   
                    case "1" : 
                        submenu = MenuFactory.GetMenu("inventory");
                        break;
                    case "2" : 
                        submenu = MenuFactory.GetMenu("store");
                        submenu.Start();
                        break;    
                    case "3" : 
                        submenu = MenuFactory.GetMenu("customer");
                        break;
                    case "4" : 
                        submenu = MenuFactory.GetMenu("order");
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                } 
            } while (repeat);
        }
    }
}