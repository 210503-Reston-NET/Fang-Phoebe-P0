using System.Collections.Generic;
using StoreModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model = StoreModels;
using Entity = StoreDL.Entities;

namespace StoreDL
{
    public class RepoDB : IRepository
    {
        private Entity.StoreAppDBContext _context;
        public RepoDB(Entity.StoreAppDBContext context)
        {
            this._context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        //Processing branch location data 
        public List<Location> GetAllLocations()
        {
            return _context.Locations
            .Select(
                obj => new Model.Location(obj.Name, obj.Address)
            ).ToList();
        }
        public Model.Location AddLocation(Model.Location location)
        {
            _context.Locations.Add(
                new Entity.Location
                {
                    Name = location.Name,
                    Address = location.Address
                }
            );
            _context.SaveChanges();
            return location;
        }
        public Model.Location GetLocation(string branchName)
        {
            Entity.Location found = _context.Locations.FirstOrDefault(obj => obj.Name == branchName);
            return (found == null) ? null : new Model.Location(found.Id, found.Name, found.Address);
        }

        //Process customer data
        public List<Customer> GetAllCustomers()
        {
            throw new System.NotImplementedException();
        }

        public Model.Customer GetCustomer(string phoneNumber)
        {
            Entity.Customer found = _context.Customers.FirstOrDefault(obj => obj.PhoneNumber == phoneNumber);
            return (found == null) ? null : new Model.Customer(found.Id, found.FirstName, found.MiddleName, found.LastName, found.PhoneNumber);
        }

        public Model.Customer GetCustomer(Customer customer)
        {
            Entity.Customer found = _context.Customers.FirstOrDefault(obj => 
                obj.LastName == customer.FullName && 
                obj.MiddleName == customer.MiddleName &&
                obj.FirstName == customer.FirstName 
                );
            return (found == null) ? null : new Model.Customer(found.Id, found.FirstName, found.MiddleName, found.LastName, found.PhoneNumber);
        }
        public Customer AddCustomer(Customer customer)
        {
            _context.Customers.Add(
                new Entity.Customer
                {
                    LastName = customer.LasttName,
                    MiddleName = customer.MiddleName,
                    FirstName = customer.FirstName,
                    PhoneNumber = customer.PhoneNumber
                }
            );
            _context.SaveChanges();
            return customer;
        }

        //Processing product data
        public Product AddProduct(Product product)
        {
            _context.Products.Add(
                new Entity.Product
                {
                    Name = product.Name,
                    Price = product.Price,
                    Barcode = product.Barcode
                }
            );
            _context.SaveChanges();
            return product;
        }

        public Model.Product GetProduct(string barcode)
        {
            Entity.Product found = _context.Products.FirstOrDefault(p => p.Barcode == barcode);
            return (found == null) ? null : new Model.Product(found.Id, found.Name, found.Price, found.Barcode);
        }
        public Product GetProductById(int id)
        {
            Entity.Product found = _context.Products.FirstOrDefault(p => p.Id == id);
            return (found == null) ? null : new Model.Product(found.Id, found.Name, found.Price, found.Barcode);
        }
        // HashSet<Product> GetAllProducts()
        // {   
        //     return null;
        // }

        //Processing inventory data
        public void AddInventory(Location location, Product product, Item item)
        {
            _context.Inventories.Add(
                new Entity.Inventory
                {
                    Quantity = item.Quantity,
                    ProductId = GetProduct(product.Barcode).Id,
                    LocationId = GetLocation(location.Name).Id
                }
            );
            _context.SaveChanges();
        }
 
       public HashSet<Item> GetAllInventories(Location location)
        {
            HashSet<Item> result = 
            (
                from i in _context.Inventories
                    join l in _context.Locations
                        on i.LocationId equals l.Id
                    join p in _context.Products
                        on i.ProductId equals p.Id
                where i.LocationId.Equals(location.Id) 
                select new Model.Item()
                {
                    Quantity = i.Quantity,
                    Product = new Model.Product()
                        {
                            Id = p.Id,
                            Barcode = p.Barcode,
                            Name = p.Name,
                            Price = p.Price
                        }
                }
            ).ToHashSet();
            return result;              
        }
        public Model.Item GetInventory(Product product)
        {
            Entity.Inventory found = _context.Inventories.FirstOrDefault(o => o.ProductId == product.Id);
            return (found == null) ? null : new Model.Item(found.Id, found.Quantity);
        }

        public void UpdateInventory(Item item, int quantity)
        {
            Entity.Inventory oldItem = _context.Inventories.Find(item.Id);
            _context.Entry(oldItem).CurrentValues.SetValues(
                oldItem.Quantity = quantity
            );
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        //To-Do
        //Update inventory by productId
        public void UpdateInventory(Product product, int quantity)
        {
            Entity.Inventory oldItem = _context.Inventories.Find(product.Id);
            _context.Entry(oldItem).CurrentValues.SetValues(
                oldItem.Quantity = quantity
            );
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }
    
        //Processing order
        public Model.Order AddOrder(Customer customer, Location location, Order order)
        {
            _context.Orders.Add(
                new Entity.Order
                {
                    OrderDate = order.OrderDate,
                    CustomerId = GetCustomer(customer.PhoneNumber).Id,
                    LocationId = GetLocation(location.Name).Id
                }
            );
            _context.SaveChanges();
            return order;
        }
        public Model.Order GetOrder(Customer customer, Location location, Order order)
        {
            Entity.Order found = _context.Orders.FirstOrDefault(o => 
                o.CustomerId == GetCustomer(customer.PhoneNumber).Id &&
                o.LocationId == GetLocation(location.Name).Id && 
                o.OrderDate == order.OrderDate);
            return (found == null) ? null : new Model.Order(found.Id, found.OrderDate, found.Total);
        }

        public Model.Order GetOrderById(Order order)
        {
            Entity.Order found = _context.Orders.FirstOrDefault(o => o.Id == order.Id);
            return (found == null) ? null : new Model.Order(found.Id, found.OrderDate, found.Total);
        }
        //update total to orders
        public void UpdateOrderTotal(Order order, decimal total)
        {
            Entity.Order oldOrder = _context.Orders.Find(order.Id);
            _context.Entry(oldOrder).CurrentValues.SetValues(
                oldOrder.Total = total
            );
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        public void AddOrderItem(Order order, Product product, Item item)
        {
            _context.OrderItems.Add(
                new Entity.OrderItem
                {
                    Quantity = item.Quantity,
                    ProductId = GetProduct(product.Barcode).Id,
                    OrderId = GetOrderById(order).Id
                }
            );
            _context.SaveChanges();

        }

        public List<Item> GetOrderItems(Order order)
        {
            List<Item> result = 
            (
                from oi in _context.OrderItems
                    join o in _context.Orders
                        on oi.OrderId equals o.Id
                    join p in _context.Products
                        on oi.ProductId equals p.Id
                where oi.OrderId.Equals(order.Id) 
                select new Model.Item()
                {
                    Quantity = oi.Quantity,
                    Product = new Model.Product()
                        {
                            Barcode = p.Barcode,
                            Name = p.Name,
                            Price = p.Price
                        }
                }
            ).ToList();
            return result;              
        }

        public List<Order> GetAllOrderByCustomer(Customer customer)
        {
            List<Order> result = 
            (
                from o in _context.Orders
                    join c in _context.Customers
                        on o.CustomerId equals c.Id
                    join l in _context.Locations
                        on o.LocationId equals l.Id
                where o.CustomerId.Equals(customer.Id) 
                select new Model.Order()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    Total = o.Total,
                    Location = new Location() {
                        Name = l.Name,
                        Address = l.Address
                    }, 
                    Customer = new Customer() {
                        FirstName = c.FirstName,
                        MiddleName = c.MiddleName,
                        LasttName = c.LastName,
                        PhoneNumber = c.PhoneNumber
                    }
                }
            ).ToList();
            return result;
        }
        public List<Order> GetAllOrderByLocation(Location location)
        {
            List<Order> result = 
            (
                from o in _context.Orders
                    join c in _context.Customers
                        on o.CustomerId equals c.Id
                    join l in _context.Locations
                        on o.LocationId equals l.Id
                where o.LocationId.Equals(location.Id)
                select new Model.Order()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    Total = o.Total,
                    Location = new Location() {
                        Name = l.Name,
                        Address = l.Address
                    }, 
                    Customer = new Customer() {
                        FirstName = c.FirstName,
                        MiddleName = c.MiddleName,
                        LasttName = c.LastName,
                        PhoneNumber = c.PhoneNumber
                    }
                }
            ).ToList();
            return result;
        }
    }
}
