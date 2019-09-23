using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { Seeds, Chicken};
public class Item : MonoBehaviour
{
    public ItemType type;
    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;
    public int maxSize;
    public InventoryManager inventory;

    public void UseItem()
    {
        switch (type)
        {
            case ItemType.Chicken:
            Debug.Log ("used a chicken..?");
                break;
            case ItemType.Seeds:
            Debug.Log ("used sum seeds");
                break;
        }
    }

    public void addItemToInventory()
    {
        Debug.Log ("invoking additem from inventorymanager");
        inventory.AddItem(this.GetComponent<Item>());
    }
}
