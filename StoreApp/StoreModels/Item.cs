namespace StoreModels
{
    /// <summary>
    /// Data Structure used to models a product and quantity
    /// </summary>
    public class Item
    {
        public Item(Product product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"Produt: {Product.ToString()} Quality: {Quantity}";
        }
    }
}