using System;
using System.Collections.Generic;
using StoreModels;
namespace StoreBL
{
    public interface IInventoryBL
    {
        //void AddInventory(Location location, Product product, Item item);
        // void AddInventory(Item item);
        void AddInventory(Location location, Product product, Item item);
        void UpdateInventory();
        Item GetInventory(Product product);
        void UpdateInventory(Item item, int quantity);
        void UpdateInventory(Product product, int quantity);

         HashSet<Item> GetAllInventories(Location location);

        HashSet<Item> GetOutOfStockInventories(Location location);
    }
}