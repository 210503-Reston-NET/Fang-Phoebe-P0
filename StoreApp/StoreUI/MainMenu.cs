using System;

namespace StoreUI
{
    public class MainMenu : IMenu
    {
        private IMenu _submenu;
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Welcome to Happy Lemon Store App");
                Console.WriteLine("[0] Exit the program");
                Console.WriteLine("[1] To Branch Menu");
                Console.WriteLine("[2] To Customer Menu");
                Console.WriteLine("[3] To Product Menu");
                Console.WriteLine("[4] To Order Menu");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0" :
                        Console.WriteLine("Thanks for using the system. Goodbye");
                        repeat = false;
                        break;   
                    case "1" : 
                        _submenu = MenuFactory.GetMenu("branch");
                        _submenu.Start();
                        break;                
                    case "2" : 
                        _submenu = MenuFactory.GetMenu("customer");
                        _submenu.Start();
                        break;    
                    case "3" : 
                        _submenu = MenuFactory.GetMenu("product");
                        _submenu.Start();
                        break; 
                    case "4" : 
                        _submenu = MenuFactory.GetMenu("order");
                        _submenu.Start();
                        break;  
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                } 
            } while (repeat);
        }
    }
}