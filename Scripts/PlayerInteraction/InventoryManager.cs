using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("인벤토리 UI")]
    public GameObject inventoryUI;
    public Transform slotsParent;
    public GameObject slotPrefab;
    public RectTransform inventoryPanel; // 인벤토리 패널 RectTransform 참조 추가

    [Header("인벤토리 설정")]
    public int inventorySize = 8; // 8개 슬롯

    [Header("UI 크기 설정")]
    public float slotSize = 64f;
    public float slotSpacing = 5f;
    public float panelPadding = 10f;

    private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    private Dictionary<int, ItemData> itemDatabase = new Dictionary<int, ItemData>();

    public static InventoryManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // UI 크기 자동 조정
        AdjustInventorySize();
        
        InitializeInventory();
        LoadItemDatabase();
        
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(true);
        }
    }

    // 인벤토리 크기 자동 조정 메서드 추가
    private void AdjustInventorySize()
    {
        if (inventoryPanel == null)
        {
            Debug.LogWarning("[InventoryManager] inventoryPanel이 할당되지 않았습니다!");
            return;
        }

        // 필요한 너비 계산
        float totalWidth = (slotSize * inventorySize) + (slotSpacing * (inventorySize - 1)) + (panelPadding * 2);
        float totalHeight = slotSize + (panelPadding * 2);

        // 패널 크기 조정
        inventoryPanel.sizeDelta = new Vector2(totalWidth, totalHeight);

        Debug.Log($"[InventoryManager] 인벤토리 크기 조정: {totalWidth} x {totalHeight}");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void InitializeInventory()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotsParent);
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            slot.Initialize(i);
            inventorySlots.Add(slot);
        }
    }

    void LoadItemDatabase()
    {
        Debug.Log("[InventoryManager] 아이템 데이터베이스 로드 시작");
        ItemData[] items = Resources.LoadAll<ItemData>("Items");
        Debug.Log($"[InventoryManager] 발견된 아이템 개수: {items.Length}개");
        
        foreach (ItemData item in items)
        {
            itemDatabase[item.itemID] = item;
            Debug.Log($"[InventoryManager] 아이템 등록: ID={item.itemID}, Name={item.itemName}");
        }
        
        Debug.Log("[InventoryManager] 아이템 데이터베이스 로드 완료");
    }

    public void ToggleInventory()
    {
        bool isActive = inventoryUI.activeSelf;
        inventoryUI.SetActive(!isActive);
    }

    // 나머지 메서드들은 동일...
    public bool AddItem(int itemID, int quantity = 1)
    {
        if (!itemDatabase.ContainsKey(itemID))
        {
            Debug.LogWarning($"[AddItem] 아이템 ID {itemID} 없음");
            return false;
        }

        ItemData itemData = itemDatabase[itemID];
        
        if (itemData.isStackable)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.HasItem() && slot.GetItemID() == itemID)
                {
                    if (slot.CanAddItem(quantity))
                    {
                        slot.AddItem(quantity);
                        return true;
                    }
                }
            }
        }

        foreach (InventorySlot slot in inventorySlots)
        {
            if (!slot.HasItem())
            {
                slot.SetItem(itemData, quantity);
                return true;
            }
        }

        return false;
    }

    public bool RemoveItem(int itemID, int quantity = 1)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.HasItem() && slot.GetItemID() == itemID)
            {
                if (slot.GetItemCount() >= quantity)
                {
                    slot.RemoveItem(quantity);
                    return true;
                }
            }
        }
        return false;
    }

    public ItemData GetItemData(int itemID)
    {
        return itemDatabase.ContainsKey(itemID) ? itemDatabase[itemID] : null;
    }

    public int GetItemTotalCount(int itemID)
    {
        int totalCount = 0;
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.HasItem() && slot.GetItemID() == itemID)
            {
                totalCount += slot.GetItemCount();
            }
        }
        return totalCount;
    }
}
