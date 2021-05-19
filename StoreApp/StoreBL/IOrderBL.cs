using System;
using System.Collections.Generic;
using StoreModels;
namespace StoreBL
{
    public interface IOrderBL
    {
        public void AddOrderItem(Order order, Product product, Item item);
        //Order AddOrder(string phoneNumber, string branchName, Order order);
        Order GetOrder(Customer customer, Location location, Order order);        Order AddOrder(Customer customer, Location location, Order order);
        void UpdateOrderTotal(Order order, decimal total);
        
        public decimal GetTotal(int id, int quantity);
        // get a list of orders
        List<Item> GetOrderItems(Order order);
        List<Order> GetAllOrderByCustomer(Customer customer, string sortingCode);
        List<Order> GetAllOrderByLocation(Location location);
    }
}