using UnityEngine;

[System.Serializable]
public class ChestSlot
{
    private ItemData currentItem;
    private int itemCount;
    
    public void SetItem(ItemData item, int count)
    {
        currentItem = item;
        itemCount = count;
    }
    
    public void ClearSlot()
    {
        currentItem = null;
        itemCount = 0;
    }
    
    public bool HasItem()
    {
        return currentItem != null && itemCount > 0;
    }
    
    public int GetItemID()
    {
        return currentItem != null ? currentItem.itemID : -1;
    }
    
    public int GetItemCount()
    {
        return itemCount;
    }
    
    public ItemData GetItemData()
    {
        return currentItem;
    }
    
    public void AddItem(int count)
    {
        itemCount += count;
    }
    
    public void RemoveItem(int count)
    {
        itemCount -= count;
        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }
}
