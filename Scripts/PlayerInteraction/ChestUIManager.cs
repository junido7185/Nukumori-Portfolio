using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestUIManager : MonoBehaviour
{
    [Header("상자 UI 설정")]
    public GameObject chestUIPanel;
    public Transform chestSlotsParent;
    public GameObject chestSlotPrefab;
    public TextMeshProUGUI chestNameText;
    public Button closeButton;
    
    [Header("그리드 설정")]
    public GridLayoutGroup gridLayoutGroup;
    
    private ChestController currentChest;
    private List<ChestSlotUI> chestSlotUIs = new List<ChestSlotUI>();
    
    public static ChestUIManager Instance { get; private set; }

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
        chestUIPanel.SetActive(false);
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseChestUI);
    }

    // 상자 UI가 열려있는지 확인
    public bool IsChestUIOpen()
    {
        return chestUIPanel.activeSelf;
    }

    public void OpenChestUI(ChestController chest)
    {
        currentChest = chest;
        chestUIPanel.SetActive(true);

        // 플레이어 이동 막기
        if (GameManager.Instance != null)
            GameManager.Instance.isAction = true;

        if (chestNameText != null)
            chestNameText.text = chest.chestData.chestName;

        CreateChestSlots(chest.chestData.width, chest.chestData.height);
    }

    public void CloseChestUI()
    {
        chestUIPanel.SetActive(false);

        // 플레이어 이동 허용
        if (GameManager.Instance != null)
            GameManager.Instance.isAction = false;

        ClearChestSlots();
        currentChest = null;
    }

    // CreateChestSlots 메서드 수정
    private void CreateChestSlots(int width, int height)
    {
        Debug.Log($"[ChestUIManager] 슬롯 생성 시작: {width}x{height}");
        
        ClearChestSlots();
        
        if (gridLayoutGroup != null)
            gridLayoutGroup.constraintCount = width;
        
        int totalSlots = width * height;
        Debug.Log($"[ChestUIManager] 생성할 슬롯 개수: {totalSlots}");
        
        if (chestSlotPrefab == null)
        {
            Debug.LogError("[ChestUIManager] chestSlotPrefab이 null입니다!");
            return;
        }
        
        for (int i = 0; i < totalSlots; i++)
        {
            GameObject slotObj = Instantiate(chestSlotPrefab, chestSlotsParent);
            ChestSlotUI slotUI = slotObj.GetComponent<ChestSlotUI>();
            
            if (slotUI != null)
            {
                Debug.Log($"[ChestUIManager] 슬롯 {i} 초기화 중...");
                slotUI.Initialize(i, currentChest);
                chestSlotUIs.Add(slotUI);
            }
            else
            {
                Debug.LogError($"[ChestUIManager] 슬롯 {i}에 ChestSlotUI 컴포넌트가 없습니다!");
            }
        }
        
        Debug.Log($"[ChestUIManager] 슬롯 생성 완료: {chestSlotUIs.Count}개");
    }


    private void ClearChestSlots()
    {
        foreach (ChestSlotUI slotUI in chestSlotUIs)
        {
            if (slotUI != null)
                Destroy(slotUI.gameObject);
        }
        chestSlotUIs.Clear();
    }
}
