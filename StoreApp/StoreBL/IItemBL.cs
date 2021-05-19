using System;
using System.Collections.Generic;
using StoreModels;
namespace StoreBL
{
    public interface IItemBL
    {
        Item AddItem(Item item);
        List<Item> GetAllItems();
    }
}