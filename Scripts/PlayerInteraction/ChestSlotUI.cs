using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ChestSlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Header("UI 요소들")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private Image background;

    private int slotIndex;
    private ChestController chestController;

    public void Initialize(int index, ChestController chest)
    {
        Debug.Log($"[ChestSlotUI] 슬롯 {index} 초기화 시작");
        slotIndex = index;
        chestController = chest;
        
        if (chestController == null)
        {
            Debug.LogError($"[ChestSlotUI] 슬롯 {index}: ChestController가 null입니다!");
            return;
        }

        // UI 요소들이 null인지 확인
        if (itemIcon == null) Debug.LogError($"[ChestSlotUI] 슬롯 {index}: itemIcon이 null!");
        if (itemCountText == null) Debug.LogError($"[ChestSlotUI] 슬롯 {index}: itemCountText가 null!");
        if (background == null) Debug.LogError($"[ChestSlotUI] 슬롯 {index}: background가 null!");

        // 수정: Raycast Target 설정 확인 및 강제 설정
        SetupRaycastTargets();
        
        UpdateUI();
        Debug.Log($"[ChestSlotUI] 슬롯 {index} 초기화 완료");
    }

    // 수정: Raycast Target 설정 메서드 추가
    private void SetupRaycastTargets()
    {
        // background Image의 Raycast Target을 활성화 (클릭 감지용)
        if (background != null)
        {
            background.raycastTarget = true;
            Debug.Log($"[ChestSlotUI] 슬롯 {slotIndex}: background raycastTarget 활성화");
        }
        
        // itemIcon과 itemCountText는 raycastTarget을 비활성화 (클릭 방해 방지)
        if (itemIcon != null)
        {
            itemIcon.raycastTarget = false;
        }
        
        // TextMeshProUGUI의 raycastTarget 설정
        if (itemCountText != null)
        {
            itemCountText.raycastTarget = false;
        }
    }

    public void UpdateUI()
    {
        if (chestController == null)
        {
            Debug.LogWarning($"[ChestSlotUI] UpdateUI: chestController가 null입니다! (슬롯: {slotIndex})");
            return;
        }

        ChestSlot slot = chestController.GetSlot(slotIndex);
        if (slot != null && slot.HasItem())
        {
            ItemData itemData = slot.GetItemData();
            Debug.Log($"[ChestSlotUI] 슬롯 {slotIndex} UI 업데이트: {itemData.itemName} x{slot.GetItemCount()}");
            
            if (itemIcon != null)
            {
                itemIcon.sprite = itemData.itemIcon;
                itemIcon.color = Color.white;
            }
            
            if (itemCountText != null)
            {
                int count = slot.GetItemCount();
                itemCountText.text = count > 1 ? count.ToString() : "";
            }
            
            Debug.Log($"[ChestSlotUI] 슬롯 {slotIndex} UI 업데이트 완료");
        }
        else
        {
            Debug.Log($"[ChestSlotUI] 슬롯 {slotIndex}는 비어있음");
            if (itemIcon != null)
            {
                itemIcon.sprite = null;
                itemIcon.color = Color.clear;
            }

            if (itemCountText != null)
            {
                itemCountText.text = "";
            }
        }
    }

    // 수정: 클릭 처리 강화
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"[ChestSlotUI] OnPointerClick 호출됨! 슬롯: {slotIndex}, 버튼: {eventData.button}");
        
        // 우클릭으로 인벤토리 이동
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log($"[ChestSlotUI] 우클릭 감지 - 인벤토리로 이동 시도");
            MoveToInventory();
        }
        // 좌클릭 테스트용 로그
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log($"[ChestSlotUI] 좌클릭 감지됨 (슬롯: {slotIndex})");
        }
    }

    // 인벤토리로 아이템 이동
    private void MoveToInventory()
    {
        Debug.Log($"[MoveToInventory] 시작 - 슬롯: {slotIndex}");
        
        if (chestController == null)
        {
            Debug.LogError("[MoveToInventory] chestController가 null입니다!");
            return;
        }

        ChestSlot slot = chestController.GetSlot(slotIndex);
        if (slot == null)
        {
            Debug.LogWarning($"[MoveToInventory] 슬롯 {slotIndex}이 존재하지 않습니다!");
            return;
        }

        if (!slot.HasItem())
        {
            Debug.LogWarning($"[MoveToInventory] 슬롯 {slotIndex}이 비어있습니다!");
            return;
        }

        ItemData itemData = slot.GetItemData();
        int count = slot.GetItemCount();
        Debug.Log($"[MoveToInventory] 시도: {itemData.itemName} x{count}");

        // InventoryManager 확인
        if (InventoryManager.Instance == null)
        {
            Debug.LogError("[MoveToInventory] InventoryManager.Instance가 null입니다!");
            return;
        }

        bool added = InventoryManager.Instance.AddItem(itemData.itemID, count);
        Debug.Log($"[MoveToInventory] InventoryManager.AddItem 반환: {added}");
        
        if (added)
        {
            slot.ClearSlot();
            UpdateUI();
            chestController.SaveChestData();
            Debug.Log($"[MoveToInventory] 이동 성공!");
        }
        else
        {
            Debug.LogWarning($"[MoveToInventory] 인벤토리에 공간 부족 또는 실패");
        }
    }

    // 드래그 관련 메서드들
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"[ChestSlotUI] 드래그 시작: 슬롯 {slotIndex}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중 처리
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log($"[ChestSlotUI] 드래그 종료: 슬롯 {slotIndex}");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"[ChestSlotUI] 드롭 이벤트: 슬롯 {slotIndex}");
        // 드롭 처리 로직
    }
}
