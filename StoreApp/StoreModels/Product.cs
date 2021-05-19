namespace StoreModels
{
    /// <summary>
    /// Data structure used to define a product
    /// </summary>
    public class Product
    {
        public Product()
        {
        }
        public Product(string name, decimal price, string barcode) 
        {
            this.Name = name;
            this.Price = price;
            this.Barcode = barcode;
        }
        
        public Product(int id, string name, decimal price, string barcode) : this(name, price, barcode)
        {
            this.Id = id;
        }
        
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Barcode { get; set; }
        public int Id { get; set; }

        //ToDo: add a category
        public override string ToString()
        {
            return $"Product Barcode:{Barcode}\tName: {Name}\tPrice: ${Price}";
        }
    }
}