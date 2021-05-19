using System;
using System.Collections.Generic;
using StoreBL;
using StoreDL;
using StoreModels;
using ConsoleTables;

namespace StoreUI
{
    public class InventoryMenu : IMenu
    {
        private IProductBL _productBL;
        private ILocationBL _locationBL;
        private IInventoryBL _inventoryBL;
        private IOrderBL _orderBL;
        private IValidationService _validate;
        
        private Location branch;
        public InventoryMenu()
        {
        }
        public InventoryMenu(ILocationBL locationBL, IProductBL productBL, IInventoryBL inventoryBL, IOrderBL orderBL, IValidationService validate)
        {
            this._locationBL = locationBL;
            this._productBL = productBL;
            this._inventoryBL = inventoryBL;
            this._orderBL = orderBL;
            this._validate = validate;
            branch = SearchBranch();
        }
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine($"Welcome to Inventory Menu for Branch ID: {branch.Id} - {branch.Name} \n\t Located at: {branch.Address}");
                Console.WriteLine("[0] Go back to main");
                Console.WriteLine("[1] view Order Histories");
                Console.WriteLine("[2] Replenish inventory");
                Console.WriteLine("[3] Add new inventory");
                Console.WriteLine("[4] View all inventories");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0" :
                        repeat = false;
                        break;  
                    case "1" :
                        ViewOrderByLocation();
                        break;
                    case "2" : 
                        ReplenishInventory();
                        break;   
                    case "3" : 
                        AddInventory(branch);
                        break;                
                    case "4" : 
                        ViewAllInventories();
                        break;     
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                } 
            } while (repeat);
        }
        private void ReplenishInventory()
        {
            //Todo- add a multiple replenishing options
            Console.WriteLine("View out of stock items and start replenishing?");
            string option = _validate.ValidateValidCommand("\tEnter [Y] to continue\n\tEnter [N] to skip");
            string productCode;
            if (option.ToLower() == "y") 
            {
                viewOutofStockItems();
                productCode = _validate.ValidateEmptyInput("Select a product from above table by Product Id");
            } else 
            {
                productCode = _validate.ValidateEmptyInput("Enter a Product Id");        
            }
            int id = Int32.Parse(productCode);
            Product product = _productBL.GetProductById(id);
            int newQuantity = _validate.ValidateQuantity("Enter quantity for this item");
            Item inventory = _inventoryBL.GetInventory(product);
            _inventoryBL.UpdateInventory(inventory, newQuantity);

            Console.WriteLine($"{newQuantity} for {product.Name} is updated.");
            Console.WriteLine("==================================================");
        }

        private void DisplayBranchLocations()
        {
            List<Location> locations = _locationBL.GetAllLocations();
            var table = new ConsoleTable("Branch Id", "Branch Location Name", "Address");

            var currentColor = Console.ForegroundColor;
            foreach(Location l in locations)
            {
                table.AddRow(l.Id, l.Name, l.Address);
                Console.ForegroundColor = ConsoleColor.Green;
            }
            table.Write();
            Console.ForegroundColor = currentColor;
        }

        private void viewOutofStockItems()
        {
            HashSet<Item> productItems = _inventoryBL.GetOutOfStockInventories(branch);
            var table = new ConsoleTable("ProductId", "Product Name", "Price");

            var currentColor = Console.ForegroundColor;
            foreach(Item i in productItems)
            {
                table.AddRow(i.Product.Id, i.Product.Name, i.Product.Price);
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            table.Write();
            Console.ForegroundColor = currentColor;
        }

        private void UpdateInventory()
        {
            Product product = _productBL.GetProductById(2);
            Item inventory = _inventoryBL.GetInventory(product);
            _inventoryBL.UpdateInventory(inventory, 10);
        }

        private Location SearchBranch()
        {
            DisplayBranchLocations();
            string name = _validate.ValidateEmptyInput("Select a location from above table by [Branch Location Name]");
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

        private void ViewOrderByLocation()
        {
            string sortingCode = _validate.ValidateEmptyInput("Enter sorting code\n\t[0] - Sort by Order Cost ASC \n\t[1] - Sort by Order Cost DESC \n\t[2] - Sort by Order Date ASC \n\t[3] - Sort By Order Date DESC");

            List<Order> orders = _orderBL.GetAllOrderByLocation(branch, sortingCode);
            // for (int i = 1; i < orders.Count; i++)
            // {
            //     Console.WriteLine($"#{i} {orders[i]}");
            // }
           var table = new ConsoleTable("Order Id", "Order Date", "Total", "Customer Name");

            var currentColor = Console.ForegroundColor;
            foreach(Order o in orders)
            {
                table.AddRow(o.Id, o.OrderDate, o.Total, o.Customer.FullName);
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            table.Write();
            Console.ForegroundColor = currentColor;
        }

        public void ViewAllInventories()
        {}
    }

}