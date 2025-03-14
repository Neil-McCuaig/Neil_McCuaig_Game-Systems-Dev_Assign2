using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemSO item;
    public int amount = 1;

    private void OnTriggerEnter(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();

        if (inventory)
        {
            Debug.Log("FUCKFUCKFUCKTHERE'SWAYTOOMANYFUCK - TheRussianBadger, health potion addict");
            inventory.AddItem(item, amount);
            Destroy(gameObject);
        }
    }
}
