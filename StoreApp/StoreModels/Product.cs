namespace StoreModels
{
    /// <summary>
    /// Data structure used to define a product
    /// </summary>
    public class Product
    {
        public Product(string productname, double price) 
        {
            this.ProductName = productname;
            this.Price = price;
        }
        public string ProductName { get; set; }

        //ToDo: validation - only have 2 decimals
        public double Price { get; set; }

        //ToDo: add a category
        public override string ToString()
        {
            return $"Product Name: {ProductName} Price: {Price}";
        }
    }
}