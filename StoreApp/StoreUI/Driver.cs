using System;
using StoreModels;
using System.Collections.Generic;

namespace StoreUI
{
    public class Driver : IMenu
    {
        public void Start()
        {
            //add a store location
            Address address = new Address("909 112th Ave NE", "#106", "Bellevue", "WA", 98004);
            Location location = new Location("French Bakery", address);
            
            //create a customer profile
            Customer c1 = new Customer("Phoebe", "", "Fang");
            
            //add inventory
            Product p1 = new Product("CupCake", 1);
            Product p2 = new Product("Chocolate Cake", 10);

            //place an order
            //order date
            DateTime orderdate = new DateTime(2021, 5, 9);
            //items to purchase
            List<Item> items = new List<Item>();
            items.Add(new Item(p1, 2));
            items.Add(new Item(p2, 1));

            //Decide later: add this to BL or model?
            // double total = 0;
            // foreach(Item item in items)
            // {
            //     total += item.Product.Price * item.Quantity;
            // }
            // Console.WriteLine(total);
            
            Order order = new Order(c1, orderdate, location, items);
            Console.WriteLine(order.ToString());
        }
    }
}