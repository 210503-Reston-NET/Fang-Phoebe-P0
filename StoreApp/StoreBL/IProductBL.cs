using System;
using System.Collections.Generic;
using StoreModels;
namespace StoreBL
{
    public interface IProductBL
    {
        Product AddProduct(Product product);
        Product GetProduct(string barcode);
        HashSet<Product> GetAllProducts();

        HashSet<Item> GetAvaliableProducts(Location location);
        Product GetProductById(int id);
    }
}