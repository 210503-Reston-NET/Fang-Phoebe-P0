using System.Collections.Generic;
using StoreModels;
using StoreDL;
using System;
using System.Linq;

namespace StoreBL
{
    public class ProductBL : IProductBL
    {
        private IRepository _repo;
        public ProductBL()
        {
        }

        public ProductBL(IRepository repo)
        {
            this._repo = repo;
        }

        public Product AddProduct(Product product)
        {
            if (GetProduct(product.Barcode) != null)
            {
                throw new Exception("The product barcode is already in our system. Please check again");
            }
            return _repo.AddProduct(product);
        }

        public HashSet<Product> GetAllProducts()
        {
            throw new System.NotImplementedException();
        }

        public Product GetProduct(string barcode)
        {
            return _repo.GetProduct(barcode);
        }

        public Product GetProductById(int id)
        {
            return _repo.GetProductById(id);
        }

        public HashSet<Item> GetAvaliableProducts(Location location)
        {
            HashSet<Item> inventories = _repo.GetAllInventories(location);
            return inventories.Where(o => o.Quantity > 0).ToHashSet();
        }

    }
}