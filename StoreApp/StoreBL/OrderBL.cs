using System;
using System.Collections.Generic;
using StoreModels;
using StoreDL;
using System.Linq;

namespace StoreBL
{
    public class OrderBL : IOrderBL
    {
        private IRepository _repo;
        private IProductBL _productBL;
        public OrderBL(IRepository repo)
        {
            this._repo = repo;
        }
        public Order AddOrder(Customer customer, Location location, Order order)
        {
            
            return _repo.AddOrder(customer, location, order);
        }

        public Order GetOrder(Customer customer, Location location, Order order)
        {
            return _repo.GetOrder(customer, location, order);
        }

        public void AddOrderItem(Order order, Product product, Item item)
        {
            _repo.AddOrderItem(order, product, item);
        }

        public decimal GetTotal(int id, int quantity)
        {
            Product product = _repo.GetProductById(id);
            return product.Price * quantity;
        }

        //combine with customer and location logic
        public void UpdateOrderTotal(Order order, decimal total)
        {
            _repo.UpdateOrderTotal(order, total);
        }

        public List<Item> GetOrderItems(Order order)
        {
            return _repo.GetOrderItems(order);
        }
        public List<Order> GetAllOrderByCustomer(Customer customer, string sortingCode)
        {
            List<Order> result =  _repo.GetAllOrderByCustomer(customer);
            switch(sortingCode)
            {
                case "0" :
                    result.Sort(delegate(Order a, Order b) {
                        return a.Total.CompareTo(b.Total);
                    });
                    break;
                case "1" :
                    result.Sort(delegate(Order a, Order b) {
                        return b.Total.CompareTo(a.Total);
                    });
                    break;
                case "2" :
                    result.Sort(delegate(Order a, Order b) {
                        return a.OrderDate.CompareTo(b.OrderDate);
                    });
                    break;
                case "3" :
                    result.Sort(delegate(Order a, Order b) {
                        return b.OrderDate.CompareTo(a.OrderDate);
                    });
                    break;
            }
            return result;
        }
        public List<Order> GetAllOrderByLocation(Location location)
        {
            return _repo.GetAllOrderByLocation(location);
        }
    }
}