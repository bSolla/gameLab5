//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                  A U T H O R  &  N O T E S
//              coded by Teresa, September 2019
//  handles the inventory slots - changes sprite, text, initiates item use
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                      V A R I A B L E S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    private Stack <Item> items;
    public Text stackText;
    public Sprite slotEmpty;
    public Sprite slotHighlight;
    public bool isEmpty
    {
        get {return items.Count == 0;}
    }

    public bool isAvailable
    {
        get {return CurrentItem.maxSize > items.Count; }
    }

    public Item CurrentItem
    {
        get {return items.Peek(); }
    }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                             M E T H O D S
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    void Start()
    {
        items = new Stack<Item>();
        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform textRect = stackText.GetComponent<RectTransform>();

        int textScaleFactor = (int)(slotRect.sizeDelta.x * 0.60);

        stackText.resizeTextMaxSize = textScaleFactor;
        stackText.resizeTextMinSize = textScaleFactor;

        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);

    }


    public void AddItem(Item item)
    {
        // adds item to slot, changes stack count if applicable
        items.Push(item);
        if (items.Count > 1)
        {
            stackText.text = items.Count.ToString();
        }

        ChangeSprite(item.spriteNeutral, item.spriteHighlighted);
    }

    private void ChangeSprite (Sprite neutral, Sprite highlight)
    {
        // changes sprite
        GetComponent<Image>().sprite = neutral;
        SpriteState st = new SpriteState();
        st.highlightedSprite = highlight;
        st.pressedSprite = neutral;
        GetComponent<Button>().spriteState = st;
    }

    public void clickTest()
    {
        Debug.Log("did a click");
        UseItemInSlot();
    }

    private void UseItemInSlot()
    {
        // use selected item
        if (!isEmpty)
        {
            items.Pop().UseItem();
            stackText.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

            if (isEmpty)
            {
                ChangeSprite(slotEmpty, slotHighlight);
                //InventoryManager.EmptySlot++;
            }
        }
    }
    

}
