using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [Header("UI 요소들")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private Image background;
    
    [Header("도트풍 스타일링")]
    [SerializeField] private Color emptySlotColor = new Color(0.3f, 0.3f, 0.3f, 0.8f);
    [SerializeField] private Color filledSlotColor = new Color(0.5f, 0.4f, 0.3f, 0.9f);

    private ItemData currentItem;
    private int itemCount;
    private int slotIndex;

    public void Initialize(int index)
    {
        slotIndex = index;
        ClearSlot();
    }

    public void SetItem(ItemData item, int count)
    {
        currentItem = item;
        itemCount = count;
        UpdateUI();
    }

    public void AddItem(int count)
    {
        itemCount += count;
        if (itemCount > currentItem.maxStackCount)
        {
            itemCount = currentItem.maxStackCount;
        }
        UpdateUI();
    }

    public void RemoveItem(int count)
    {
        itemCount -= count;
        if (itemCount <= 0)
        {
            ClearSlot();
        }
        else
        {
            UpdateUI();
        }
    }

    public bool CanAddItem(int count)
    {
        return currentItem != null && (itemCount + count) <= currentItem.maxStackCount;
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

    // 수정: 도트풍 스타일 적용
    private void UpdateUI()
    {
        if (currentItem != null)
        {
            // 아이템이 있을 때
            itemIcon.sprite = currentItem.itemIcon;
            itemIcon.color = Color.white;
            background.color = filledSlotColor;
            
            // 숫자 표시 (도트폰트 스타일링)
            if (itemCountText != null)
            {
                itemCountText.text = itemCount > 1 ? itemCount.ToString() : "";
                itemCountText.color = Color.white;
                itemCountText.fontStyle = FontStyles.Bold;
            }
        }
        else
        {
            // 빈 슬롯일 때
            if (itemIcon != null)
            {
                itemIcon.sprite = null;
                itemIcon.color = Color.clear;
            }
            
            if (background != null)
            {
                background.color = emptySlotColor;
            }
            
            if (itemCountText != null)
            {
                itemCountText.text = "";
            }
        }
    }

    private void ClearSlot()
    {
        currentItem = null;
        itemCount = 0;
        UpdateUI();
    }
}
