using System.Collections.Generic;
using StoreModels;
namespace StoreDL
{
    public interface IRepository
    {
        //Processing branch location data 
        List<Location> GetAllLocations();
        Location AddLocation(Location location);
        Location GetLocation(string branchName);

        //Process customer data
        List<Customer> GetAllCustomers();
        Customer GetCustomer(string phoneNumber);
        Customer GetCustomer(Customer customer);
        Customer AddCustomer(Customer customer);

        //Processing product data
        Product AddProduct(Product product);
        Product GetProduct(string barcode);

        //HashSet<Item> GetAllProductItems(Location location);
        Product GetProductById(int id);
        void UpdateInventory(Product product, int quantity);

        //Processing inventory data
        void AddInventory(Location location, Product product, Item item);
        HashSet<Item> GetAllInventories(Location location);
        Item GetInventory(Product product);
        void UpdateInventory(Item item, int quanity);

        //Processing order data
        Order AddOrder(Customer customer, Location location, Order order);
        void UpdateOrderTotal(Order order, decimal total);
        Order GetOrder(Customer customer, Location location, Order order); 
        Order GetOrderById(Order order);
        void AddOrderItem(Order order, Product product, Item item);

        //List<Item> GetOrderItems(Order order);
        List<Item> GetOrderItems(Order order);
        List<Order> GetAllOrderByCustomer(Customer customer);
        List<Order> GetAllOrderByLocation(Location location);
    }
}