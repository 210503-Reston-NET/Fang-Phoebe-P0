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
                Console.WriteLine("[1] Customer Sign In");
                Console.WriteLine("[2] New Customer Sign Up");
                Console.WriteLine("[3] Employee Sign In");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0" :
                        Console.WriteLine("Thanks for using the system. Goodbye");
                        repeat = false;
                        break;   
                    case "1" : 
                        _submenu = MenuFactory.GetMenu("order");
                        _submenu.Start();
                        break;             
                    case "2" : 
                        _submenu = MenuFactory.GetMenu("customer");
                        _submenu.Start();
                        break;    
                    case "3" : 
                        _submenu = MenuFactory.GetMenu("branch");
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