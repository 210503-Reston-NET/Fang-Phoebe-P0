using System;
using System.Collections.Generic;
using StoreBL;
using StoreDL;
using StoreModels;
using ConsoleTables;
namespace StoreUI
{
    public class OrderMenu : IMenu
    {
        private ICustomerBL _customerBL;
        private ILocationBL _locationBL;
        private IProductBL _productBL;
        private IInventoryBL _inventoryBL;
        private IOrderBL _orderBL;
        private IValidationService _validate;
   
        public OrderMenu(ICustomerBL customerBL, ILocationBL locationBL, IProductBL productBL, IInventoryBL inventoryBL, IOrderBL orderBL, IValidationService validate)
        {
            _customerBL = customerBL;
            _locationBL = locationBL;
            _productBL = productBL;
            _inventoryBL = inventoryBL;
            _orderBL = orderBL;
            _validate = validate;
        }

        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("[0] Go back to main");
                Console.WriteLine("[1] Place a new order");
                Console.WriteLine("[2] View order Histories customer");
                Console.WriteLine("[3] View order Histories by location");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0" :
                        repeat = false;
                        break;   
                    case "1" : 
                        PlaceOrders();
                        break;                
                    case "2" : 
                        //ViewOrders();
                        DisplayProducts(new Location());
                        //ViewOrderByCustomer();
                        break;    
                    case "3" : 
                        //
                        GetInventory();
                        ViewOrderByLocation();
                        break;  
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                } 
            } while (repeat);
        }

        private void ViewOrders()
        {
            throw new NotImplementedException();
        }

        private void DisplayProducts(Location location)
        {
            //location = SearchBranch();
            HashSet<Item> productItems = _productBL.GetAvaliableProducts(location);
            var table = new ConsoleTable("ProductId", "Product Name", "Price", "Left in Stock");

            var currentColor = Console.ForegroundColor;
            foreach(Item i in productItems)
            {
                table.AddRow(i.Product.Id, i.Product.Name, i.Product.Price, i.Quantity);
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            table.Write();
            Console.ForegroundColor = currentColor;
        }
        private void PlaceOrders()
        {
            Customer customer = SearchCustomer();
            Location location = SearchBranch();

            //loction --> inventory --> product (locationId)

            //Todo 
            //Display a list of product avaiable products in the selected store
            DisplayProducts(location);

            //ToDo - change date to timestamp instead of input
            //DateTime orderDate = _validate.ValidateDate("Enter the order date [yyyy-mm-dd]:");
            DateTime orderDate = new DateTime();
            Order newOrder = new Order(orderDate);
            Order createdOrder = _orderBL.AddOrder(customer, location, newOrder);
            Order curOrder = _orderBL.GetOrder(customer, location, newOrder);
        
            decimal total = 0;
            string input;



            total += AddItem(curOrder);
            Console.WriteLine($"Current Total: {total}");
            input = _validate.ValidateValidCommand("More items to add? \n\t[Y] - continue \n\t[N] - proceed order");

            while (input.ToLower() != "n")
            {
                total += AddItem(curOrder);
                Console.WriteLine($"Current Total: {total}");
                input = _validate.ValidateValidCommand("More items to add? \n\t[Y] - continue \n\t[N] - proceed order");
            }
            
            //To-Do add timestamp to find the order
            _orderBL.UpdateOrderTotal(curOrder, total);
            Console.WriteLine("Order has been placed successfully");
            Console.WriteLine("====================================================");
            Console.WriteLine($"Here is the Order Details for {customer.FullName} \n\tOrderID: {curOrder.Id} Total: ${total} Location: {location.Name}");
            ViewOrderDetails(curOrder);
            Console.WriteLine("====================================================");
        }

        private decimal AddItem(Order order)
        {
            string productCode = _validate.ValidateEmptyInput("Please enter the [Product Id] for the item you want to buy");
            int id = Int32.Parse(productCode);

            int quantity = _validate.ValidateQuantity("Enter quantity for this item");
            Product product = _productBL.GetProductById(id);

            //To-Do check enough inventory
            Item inventory = _inventoryBL.GetInventory(product);
            _orderBL.AddOrderItem(order, product, new Item(quantity));

            //To-Do update inventory
            int newQuantity = inventory.Quantity - quantity;
            _inventoryBL.UpdateInventory(inventory, newQuantity);

            Console.WriteLine($"{quantity} {product.Name} is added");
            return //product.Price * quantity;
            
            _orderBL.GetTotal(product.Id, quantity);
        }

        private Customer SearchCustomer()
        {
            string phoneNumber = _validate.ValidateEmptyInput("Enter the customer phone number: ");
            try
            {
                Customer customer = _customerBL.GetCustomer(phoneNumber);
                return customer;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("No matched branch is found. Go back to the Menu and create one");
                return null;
            }
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

        private void ViewOrderDetails(Order order)
        {
            List<Item> orderItems = _orderBL.GetOrderItems(order);
            //orderItems.ForEach(i => Console.WriteLine(i.ToString()));
            for (int i = 1; i < orderItems.Count; i++)
            {
                Console.WriteLine($"#{i} {orderItems[i]}");
            }
        }

        private void ViewOrderByCustomer()
        {
            Customer customer = SearchCustomer();
            string sortingCode = _validate.ValidateEmptyInput("Enter sorting code\n\t[0] - Sort by Order Cost ASC \n\t[1] - Sort by Order Cost DESC \n\t[2] - Sort by Order Date ASC \n\t[3] - Sort By Order Date DESC");
            List<Order> orders = _orderBL.GetAllOrderByCustomer(customer, sortingCode);
            
            //var table = new ConsoleTable();
            for (int i = 1; i < orders.Count; i++)
            {
                Console.WriteLine($"#{i} {orders[i]}");
            }
        }
        private void ViewOrderByLocation()
        {
            Location location = SearchBranch();
            List<Order> orders = _orderBL.GetAllOrderByLocation(location);
            for (int i = 1; i < orders.Count; i++)
            {
                Console.WriteLine($"#{i} {orders[i]}");
            }
        }

        private void GetInventory()
        {
            Product p = _productBL.GetProductById(2);
            Item i = _inventoryBL.GetInventory(p);
            Console.WriteLine(i.Quantity);
            Console.WriteLine(p.Name);

        }
    }

}