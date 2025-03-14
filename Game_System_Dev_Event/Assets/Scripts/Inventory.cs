using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]

//public class Item
//{

//string itemName;
//string description;
//int value;
//}

public class Inventory : MonoBehaviour
{

    //Health stuff
    //public int maxHealth = 100;
    //public int currentHealth;

    public HealthBar healthBar;


    public Movement player;

    //ItemSO is the key, int is the value, you think?
    public Dictionary<ItemSO, int> inventory = new Dictionary<ItemSO, int>();
    public int maxInventorySlots = 50;

    public ItemSO healthPot;

    //This is a delegate.
    public delegate void OnInventoryChange();
    public event OnInventoryChange InventoryUpdated;

    public string savePath;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "Inventory.json");

        //currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
    }
    public void AddItem(ItemSO newItem, int amount)
    {
        //Say newitem is a potion, and you have a potion in your inventory. This adds the new potion to the old potion stack.
        if (inventory.ContainsKey(newItem))
        {
            inventory[newItem] += amount;
        }
        else
        {
            if (inventory.Count >= maxInventorySlots)
            {
                Debug.Log("Inventory is full");
                return;
            }
            inventory.Add(newItem, amount);
        }
        //Update UI here
        InventoryUpdated?.Invoke();

        //currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);

    }

    public void RemoveItem(ItemSO itemToRemove, int amount) 
    { 
        //If it exists and there is more than one of it, decrease the amount
        if (inventory.ContainsKey(itemToRemove))
        {
            inventory[itemToRemove] -= amount;
            if (inventory[itemToRemove] <= 0)
            {
                //This removes it from the dictionary entirely if nothing is present.
                inventory.Remove(itemToRemove);
            }
        }

    }

    public bool HasItem(ItemSO item, int amount)
    {
        return inventory.ContainsKey(item) && inventory[item] >= amount;
    }

    public void chugPotion()
    {
        if (HasItem(healthPot, 1))
        {
            Debug.Log("HasItem checks");
            player.currentHealth = player.currentHealth + 20;
            RemoveItem(healthPot, 1);

            InventoryUpdated?.Invoke();
            healthBar.SetHealth(player.currentHealth);
        }
    }

    //public void TakeDamage(int damage)
    //{
        //currentHealth -= damage;    

        //healthBar.SetHealth(currentHealth);
    //}

    //Save to Json file
    public void SaveInventory()
    {
        InventoryData data = new InventoryData(inventory);

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(savePath, json);
        Debug.Log("Inventory save to " + savePath);
    }

    public void LoadInventory()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);
            Debug.Log(json);
            inventory = data.ToDictionary();
            //Debug.Log(data.itemNames[0]);
            //Update inventory UI here
            InventoryUpdated?.Invoke();
            Debug.Log("inventory loaded from : " + savePath);
        }
        else
        {
            Debug.Log("No save file found");
        }
    }

}
