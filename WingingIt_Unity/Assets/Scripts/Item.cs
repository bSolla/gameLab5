//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                  A U T H O R  &  N O T E S
//              coded by Teresa, September 2019
//          simple script to handle different items
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ItemType { Seeds, Chicken};
public class Item : MonoBehaviour
{
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                      V A R I A B L E S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public ItemType type;
    public Sprite spriteNeutral;
    public Sprite spriteHighlighted;
    public int maxSize;
    public InventoryManager inventory;
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                             M E T H O D S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    public void UseItem()
    {
        // uses the selected item
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
        // used this for testing, 
        // might need to be changed depending on how we end up doing "buying"
        Debug.Log ("invoking additem from inventorymanager");
        inventory.AddItem(this.GetComponent<Item>());
    }
}
