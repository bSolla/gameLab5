using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

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
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item item)
    {
        items.Push(item);
        if (items.Count > 1)
        {
            stackText.text = items.Count.ToString();
        }

        ChangeSprite(item.spriteNeutral, item.spriteHighlighted);
    }

    private void ChangeSprite (Sprite neutral, Sprite highlight)
    {
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
        // clicked the slot, in inventory this should be used to select/use the item,
        // but in the shop this should be used to attempt to buy the item.
        // easiest way is probably to make two seperate (but very similar) scripts
    }

    private void UseItemInSlot()
    {
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
