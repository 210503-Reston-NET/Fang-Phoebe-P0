using System;
using System.Collections.Generic;

namespace StoreModels
{
    /// <summary>
    /// Data structure used to define an order
    /// </summary>
    public class Order
    {
        private double _total;
        public Order(Customer customer, DateTime orderdate, Location location, List<Item> items)
        {
            //this.Total = item.Product.Price * item.Quantity;
            this.Customer = customer;
            this.OrderDate = orderdate;
            this.Location = location;
            this.Items = items;
            this.Total = _total;
        }

        public Customer Customer { get; set; }
        public Location Location { get; set; }
        public List<Item> Items { get; set; }
        public double Total 
        { 
            get { return _total; }
            set 
            {
                //_total = Item.Product.Price * Item.Quantity;
            foreach(Item item in Items)
            {
                _total += item.Product.Price * item.Quantity;
            }      
          }
        }
        public DateTime OrderDate { get; set; }

        //ToDo: view details of an order
        public override string ToString()
        {
            return $"\nCustomer Name: {Customer.FullName} \nTotal: {Total} Location: {Location.ToString()}";
        }
    }
}