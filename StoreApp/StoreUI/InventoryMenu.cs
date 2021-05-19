using System;
using System.Collections.Generic;
using StoreBL;
using StoreDL;
using StoreModels;
namespace StoreUI
{
    public class InventoryMenu : IMenu
    {
        private IProductBL _productBL;
        private ILocationBL _locationBL;
        private IInventoryBL _inventoryBL;
        private IValidationService _validate;
        public InventoryMenu()
        {
        }
        public InventoryMenu(ILocationBL locationBL, IProductBL productBL, IInventoryBL inventoryBL, IValidationService validate)
        {
            this._locationBL = locationBL;
            this._productBL = productBL;
            this._inventoryBL = inventoryBL;
            this._validate = validate;
        }
        public void Start()
        {
            bool repeat = true;
            Location branch = SearchBranch();
            do
            {
                Console.WriteLine($"Welcome to Inventory Menu for Branch ID: {branch.Id} - {branch.Name} \n\t Located at: {branch.Address}");
                Console.WriteLine("[0] Go back to main");
                Console.WriteLine("[1] Add new inventory");
                Console.WriteLine("[2] Replenish inventory");
                Console.WriteLine("[3] View all inventories");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0" :
                        repeat = false;
                        break;   
                    case "1" : 
                        AddInventory(branch);
                        break;                
                    case "2" : 
                        UpdateInventory();
                        break;    
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                } 
            } while (repeat);
        }

        private void UpdateInventory()
        {
            throw new NotImplementedException();
        }
        private Location SearchBranch()
        {
            string name = _validate.ValidateEmptyInput("Enter the branch location name: ");
            try
            {
                Location branch = _locationBL.GetLocation(name);
                return branch;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("No matched branch is found. Go back to Branch Menu and create one");
                return null;
            }
        }
        
        private void AddInventory(Location location)
        {
            //ToDo - entery name as well?
            string barcode = _validate.ValidateEmptyInput("Enter product barcode");
            int quantity = _validate.ValidateQuantity("Enter quantity for this item");
     
            // ToDo - check if new product has inventory already
            // Display a list of new product which has no quantity
            Product product = _productBL.GetProduct(barcode);

            _inventoryBL.AddInventory(location, product, new Item(quantity));
            Console.WriteLine($"{product.Name}'s quantity is added");
        }
    }

}