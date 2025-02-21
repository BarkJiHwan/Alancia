using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour 
{    
    public Image itemImage;
    public Sprite slotImage;
    private ItemInfo item;
    private int slotIndex;
    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }
    public bool HasItem()
    {
        return item != null;
    }
    public void SetItem(ItemInfo newItem)
    {        
        itemImage.sprite = newItem.itemSprite;
        itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 1f);
    }
    public void ClearItem()
    {
        //item = null;
        itemImage.sprite = slotImage;
        itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 0f);
    }
}