using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [Header("상자 설정")]
    public ChestData chestData;
    public bool isOpened = false;

    [Header("상자 상태")]
    private List<ChestSlot> chestSlots = new List<ChestSlot>();
    private bool isFirstOpen = true;

    private void Start()
    {
        InitializeChest();
    }

    // 상자 초기화 - 수정됨
    private void InitializeChest()
    {
        Debug.Log($"[ChestController] 상자 초기화 시작");
        Debug.Log($"[ChestController] ChestData: {chestData?.chestName} (ID: {chestData?.chestID})");
        
        if (chestData == null)
        {
            Debug.LogError("[ChestController] ChestData가 null입니다!");
            return;
        }

        string saveKey = "Chest_" + chestData.chestID;
        
        // 수정: PlayerPrefs 검증 로직 개선
        bool hasOpenedKey = PlayerPrefs.HasKey(saveKey + "_opened");
        bool hasValidSlotData = false;
        
        // 슬롯 데이터가 실제로 존재하는지 확인
        if (hasOpenedKey)
        {
            for (int i = 0; i < chestData.width * chestData.height; i++)
            {
                if (PlayerPrefs.HasKey(saveKey + "_slot_" + i + "_itemID"))
                {
                    hasValidSlotData = true;
                    break;
                }
            }
        }
        
        // 실제로 유효한 데이터가 있을 때만 로드, 아니면 새로 생성
        isFirstOpen = !hasOpenedKey || !hasValidSlotData;
        Debug.Log($"[ChestController] isFirstOpen: {isFirstOpen} (HasKey: {hasOpenedKey}, HasValidData: {hasValidSlotData})");
        
        if (isFirstOpen)
        {
            // PlayerPrefs를 완전히 클리어하고 새로 시작
            ClearChestData();
            GenerateInitialItems();
        }
        else
        {
            LoadChestData();
        }

        Debug.Log($"[ChestController] 초기화 완료. 총 슬롯 개수: {chestSlots.Count}");
    }

    // 수정: 상자 데이터 클리어 메서드 추가
    private void ClearChestData()
    {
        string saveKey = "Chest_" + chestData.chestID;
        int totalSlots = chestData.width * chestData.height;
        
        // 기존 PlayerPrefs 데이터 모두 삭제
        PlayerPrefs.DeleteKey(saveKey + "_opened");
        for (int i = 0; i < totalSlots; i++)
        {
            PlayerPrefs.DeleteKey(saveKey + "_slot_" + i + "_itemID");
            PlayerPrefs.DeleteKey(saveKey + "_slot_" + i + "_count");
        }
        
        // 슬롯 리스트 초기화
        chestSlots.Clear();
        Debug.Log($"[ChestController] 상자 데이터 클리어 완료");
    }

    // 초기 아이템들 생성 (확률 적용)
    private void GenerateInitialItems()
    {
        Debug.Log($"[ChestController] 초기 아이템 생성 시작");
        Debug.Log($"[ChestController] 초기 아이템 리스트 크기: {chestData.initialItems.Count}");
        
        foreach (ChestData.ChestItem chestItem in chestData.initialItems)
        {
            if (chestItem.item == null)
            {
                Debug.LogWarning("[ChestController] 초기 아이템이 null입니다!");
                continue;
            }

            Debug.Log($"[ChestController] 아이템 확률 체크: {chestItem.item.itemName}, 확률: {chestItem.dropChance}%");
            float randomValue = Random.Range(0f, 100f);
            Debug.Log($"[ChestController] 랜덤 값: {randomValue}");
            
            if (randomValue <= chestItem.dropChance)
            {
                Debug.Log($"[ChestController] 아이템 생성: {chestItem.item.itemName} x{chestItem.quantity}");
                AddItemToRandomSlot(chestItem.item, chestItem.quantity);
            }
            else
            {
                Debug.Log($"[ChestController] 아이템 생성 실패 (확률)");
            }
        }
    }

    // 나머지 메서드들은 그대로 유지...
    private void AddItemToRandomSlot(ItemData item, int quantity)
    {
        Debug.Log($"[ChestController] AddItemToRandomSlot 시작: {item.itemName} x{quantity}");
        int totalSlots = chestData.width * chestData.height;
        List<int> availableSlots = new List<int>();
        
        for (int i = 0; i < totalSlots; i++)
        {
            if (i >= chestSlots.Count || !chestSlots[i].HasItem())
            {
                availableSlots.Add(i);
            }
        }

        Debug.Log($"[ChestController] 사용 가능한 슬롯 개수: {availableSlots.Count}/{totalSlots}");
        
        if (availableSlots.Count > 0)
        {
            int randomSlot = availableSlots[Random.Range(0, availableSlots.Count)];
            while (chestSlots.Count <= randomSlot)
            {
                chestSlots.Add(new ChestSlot());
            }

            chestSlots[randomSlot].SetItem(item, quantity);
            Debug.Log($"[ChestController] 슬롯 {randomSlot}에 아이템 추가 성공");
        }
        else
        {
            Debug.LogWarning("[ChestController] 사용 가능한 슬롯이 없습니다!");
        }
    }

    // 상자 열기
    public void OpenChest()
    {
        if (!isOpened)
        {
            isOpened = true;
            if (isFirstOpen)
            {
                isFirstOpen = false;
                SaveChestData();
            }
        }

        if (ChestUIManager.Instance != null)
        {
            ChestUIManager.Instance.OpenChestUI(this);
        }
    }

    // 상자 데이터 저장
    public void SaveChestData()
    {
        string saveKey = "Chest_" + chestData.chestID;
        PlayerPrefs.SetInt(saveKey + "_opened", 1);
        
        for (int i = 0; i < chestSlots.Count; i++)
        {
            if (chestSlots[i].HasItem())
            {
                PlayerPrefs.SetInt(saveKey + "_slot_" + i + "_itemID", chestSlots[i].GetItemID());
                PlayerPrefs.SetInt(saveKey + "_slot_" + i + "_count", chestSlots[i].GetItemCount());
            }
            else
            {
                PlayerPrefs.DeleteKey(saveKey + "_slot_" + i + "_itemID");
                PlayerPrefs.DeleteKey(saveKey + "_slot_" + i + "_count");
            }
        }
        PlayerPrefs.Save();
    }

    // 상자 데이터 로드
    private void LoadChestData()
    {
        string saveKey = "Chest_" + chestData.chestID;
        int totalSlots = chestData.width * chestData.height;
        
        for (int i = 0; i < totalSlots; i++)
        {
            string itemKey = saveKey + "_slot_" + i + "_itemID";
            string countKey = saveKey + "_slot_" + i + "_count";
            
            if (PlayerPrefs.HasKey(itemKey))
            {
                int itemID = PlayerPrefs.GetInt(itemKey);
                int count = PlayerPrefs.GetInt(countKey);
                
                while (chestSlots.Count <= i)
                {
                    chestSlots.Add(new ChestSlot());
                }

                ItemData itemData = InventoryManager.Instance?.GetItemData(itemID);
                if (itemData != null)
                {
                    chestSlots[i].SetItem(itemData, count);
                }
            }
        }
    }

    // 특정 슬롯 가져오기
    public ChestSlot GetSlot(int index)
    {
        if (index >= 0 && index < chestSlots.Count)
            return chestSlots[index];
        return null;
    }

    // 상자에 아이템 추가
    public bool AddItemToChest(ItemData item, int quantity)
    {
        if (item.isStackable)
        {
            foreach (ChestSlot slot in chestSlots)
            {
                if (slot.HasItem() && slot.GetItemID() == item.itemID)
                {
                    int currentCount = slot.GetItemCount();
                    int maxStack = item.maxStackCount;
                    int canAdd = maxStack - currentCount;
                    
                    if (canAdd > 0)
                    {
                        int addAmount = Mathf.Min(canAdd, quantity);
                        slot.AddItem(addAmount);
                        quantity -= addAmount;
                        
                        if (quantity <= 0)
                        {
                            SaveChestData();
                            return true;
                        }
                    }
                }
            }
        }

        int totalSlots = chestData.width * chestData.height;
        for (int i = 0; i < totalSlots; i++)
        {
            if (i >= chestSlots.Count)
            {
                chestSlots.Add(new ChestSlot());
            }

            if (!chestSlots[i].HasItem())
            {
                chestSlots[i].SetItem(item, quantity);
                SaveChestData();
                return true;
            }
        }

        return false;
    }
}
