using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class InventoryData 
{
    
    public List<string> itemNames = new List<string>(); //Stores item names
    public List<int> itemQuantities = new List<int>(); //Stores item quantities


    //A "Constructor", loads up a default
    public InventoryData(Dictionary<ItemSO, int> inventory)
    {
        foreach(var entry in inventory)
        {
            itemNames.Add(entry.Key.itemName);
            itemQuantities.Add(entry.Value);
        }
    }

    public Dictionary<ItemSO, int> ToDictionary()
    {
        Dictionary<ItemSO, int> inventory = new Dictionary<ItemSO, int>();

        foreach (ItemSO item in Resources.LoadAll<ItemSO>("Scriptable Objects"))
        {
            for (int i = 0; i < itemNames.Count; i++) 
            { 
                Debug.Log(itemNames.Count);

                if (item.itemName == itemNames[i])
                {
                    if (!inventory.ContainsKey(item))
                    {
                        inventory[item] = itemQuantities[i];
                    }
                    else
                    {
                        inventory[item] += itemQuantities[i];
                    }
                }
            }

        }
        Debug.Log("Restored items from save " + inventory.Count);
        return inventory;
    }
}
