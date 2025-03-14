using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Transform inventoryPanel;
    public GameObject inventorySlotPrefab;

    private List<GameObject> slots = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();

        DontDestroyOnLoad(this.gameObject);

        //Whenever UpdateUI is called, this happens. It's called "Subscribing". You need to have an unsubscribe to make sure things don't bork. Bork is a memory leak.
        inventory.InventoryUpdated += UpdateUI;

        //GameManager.Instance.LoadScene("Level 1");


    }

    private void OnDestroy()
    {
        inventory.InventoryUpdated -= UpdateUI;
    }

    //In some situations you may need to put a match to whater it's subbed to into the (), so int amount in this instance
    private void UpdateUI()
    {
        //Clear old slots, then create new slots from inventory dictionary

        foreach (var slot in slots)
        {
            Destroy(slot);

        }

        slots.Clear();
        
        foreach (KeyValuePair<ItemSO, int> entry in inventory.inventory)
        {
            //getting the inventory from the script named inventory
            GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryPanel);
            //The below may need specific references to different bits of text in the inventory
            newSlot.GetComponentInChildren<TextMeshProUGUI>().text = entry.Key.itemName + " :" + entry.Value;
            newSlot.GetComponentInChildren<Image>().sprite = entry.Key.itemImage;
            slots.Add(newSlot);
        }
    }
}
