namespace StoreModels
{
    /// <summary>
    /// Data Structure used to models a product and quantity
    /// </summary>
    public class Item
    {
        public Item()
        {}

        public Item(int Quantity)
        {
            this.Quantity = Quantity;
        }
        public Item(int id, int quantity) : this(quantity)
        {
            this.Id = id;
        }
        public int Id { get; set; }

        public int Quantity { get; set; }

        public Product Product {get; set;}

        public override string ToString()
        {
            return $"\t\t{Product.ToString()}\tQTY: {Quantity}\tAmount: ${Product.Price * Quantity}";
        }
    }
}