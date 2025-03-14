using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]

public class ItemSO : ScriptableObject
{
    public Sprite itemImage;
    public string itemName;
    public string itemDescription;
    public int maxStackSize = 100;
    public bool isConsumable;
}
