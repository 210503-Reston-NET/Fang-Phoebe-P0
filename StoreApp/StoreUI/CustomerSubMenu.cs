using System;
namespace StoreUI
{
    public class CustomerSubMenu  : IMenu
    {
        private IMenu submenu;
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("You're accessing Customername's account");
                Console.WriteLine("[0] Go back to the main");
                Console.WriteLine("[1] Place an order");
                Console.WriteLine("[2] View order hisotry");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0" :
                        repeat = false;
                        break;   
                    case "1" : 
                        submenu = MenuFactory.GetMenu("order");
                        submenu.Start();
                        break;                
                    case "2" : 
                        viewOrderHistory();
                        break;    
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                } 
            } while (repeat);
        }

        private void viewOrderHistory()
        {
            throw new NotImplementedException();
        }
    }
}