using System;
using System.Collections.Generic;
using StoreModels;
using StoreDL;
using System.Linq;

namespace StoreBL
{
    public class InventoryBL : IInventoryBL
    {
        private IRepository _repo;
        public InventoryBL(IRepository repo)
        {
            this._repo = repo;
        }

        public void AddInventory(Location location, Product product, Item item)
        {
            _repo.AddInventory(location, product, item);
        }

        public Item GetInventory(Product product)
        {
           return _repo.GetInventory(product);
        }
        public void UpdateInventory()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateInventory(Item item, int quantity)
        {
            _repo.UpdateInventory(item, quantity);
        }
        public void UpdateInventory(Product product, int quantity)
        {
            _repo.UpdateInventory(product, quantity);
        }
        
        public HashSet<Item> GetAllInventories(Location location)
        {
            return _repo.GetAllInventories(location);
        }

        public HashSet<Item> GetOutOfStockInventories(Location location)
        {
            HashSet<Item> inventories = _repo.GetAllInventories(location);
            return inventories.Where(o => o.Quantity <= 0).ToHashSet();
        }
    }
}