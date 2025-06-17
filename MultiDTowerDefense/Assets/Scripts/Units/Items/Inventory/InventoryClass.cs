using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryClass 
{
    public List<ItemClass> Inventory = new List<ItemClass>();

    public void AddToInventory(ItemClass item) 
   
    {
        Inventory.Add(item);
    }
}
