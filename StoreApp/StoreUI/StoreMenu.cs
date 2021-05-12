using System;
using System.Collections.Generic;
using StoreBL;
using StoreDL;
using StoreModels;
namespace StoreUI
{
    public class StoreMenu : IMenu
    {
        private ILocationBL _locationBL;
        public StoreMenu(ILocationBL locationBL)
        {
            this._locationBL = locationBL;
        }
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Store Management");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[0] Go back to main");
                Console.WriteLine("[1] View all locations");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        repeat = false;
                        break;
                    case "1":
                        ViewAllLocations();
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            } while (repeat);
        }

        public void ViewAllLocations() 
        {
            List<Location> locations = _locationBL.GetAllLocations();
            Action<Object> print = o => Console.WriteLine(o.ToString());
            if (locations.Count == 0) Console.WriteLine("No restaurants :< You should add some");
            else
            {
                locations.ForEach(print);
            }
        }
    }
}