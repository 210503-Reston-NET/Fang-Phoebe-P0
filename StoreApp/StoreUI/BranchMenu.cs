using System;
using System.Collections.Generic;
using StoreBL;
using StoreDL;
using StoreModels;
namespace StoreUI
{
    public class BranchMenu : IMenu
    {
        private ILocationBL _locationBL;
        private IValidationService _validate;
        public BranchMenu(ILocationBL locationBL, IValidationService validate)
        {
            this._locationBL = locationBL;
            this._validate = validate;
        }
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("You're on store branch Menu");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[0] Go back to main");
                Console.WriteLine("[1] Add a new branch location");
                Console.WriteLine("[2] Search a branch location");
                Console.WriteLine("[3] View all branch locations");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        repeat = false;
                        break;
                    case "1":
                        AddLocation();
                        break;
                    case "2":
                        MenuFactory.GetMenu("inventory").Start();
                        break;
                    case "3":
                        ViewAllLocations();
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            } while (repeat);
        }

        private void AddLocation()
        {
            string name = _validate.ValidateEmptyInput("Enter Location Name:");
            string address = _validate.ValidateEmptyInput("Enter Location Address:");
            try
            {
                Location newBranch = new Location(name, address);
                Location createdBranch = _locationBL.AddLocation(newBranch);
                Console.WriteLine($"New {createdBranch.Name} branch is created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ViewAllLocations() 
        {
            List<Location> locations = _locationBL.GetAllLocations();
            Action<Object> print = o => Console.WriteLine(o.ToString());
            if (locations.Count == 0) Console.WriteLine("No location :< You should add some");
            else
            {
                locations.ForEach(print);
            }
        }
    }
}