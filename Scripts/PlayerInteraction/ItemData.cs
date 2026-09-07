using UnityEngine;  // Unity 기본 기능 사용하기 위해 필요

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
// ↑ 이 한 줄이 Unity 에디터에서 우클릭 → Create → Inventory → Item 메뉴를 만들어줌
public class ItemData : ScriptableObject  // ScriptableObject를 상속받아야 데이터 저장소가 됨
{
    [Header("아이템 기본 정보")]  // Inspector에서 구분선 역할
    public int itemID;           // 아이템을 구별하는 고유 번호 (1=포션, 2=검, 3=방패 등)
    public string itemName;      // 아이템 이름 ("체력 포션", "강철 검" 등)
    public Sprite itemIcon;      // 인벤토리에 표시될 아이템 그림
    public string description;   // 아이템 설명 ("체력을 50 회복합니다")
    
    [Header("아이템 속성")]
    public ItemType itemType;    // 아이템 종류 (소모품, 장비, 재료 등)
    public int maxStackCount = 99;    // 한 슬롯에 몇 개까지 쌓을 수 있는지
    public bool isStackable = true;   // 쌓을 수 있는 아이템인지 여부
}

public enum ItemType {
    Consumable,    // 0번: 소모품 (포션, 음식 등)
    Equipment,     // 1번: 장비 (검, 방패, 갑옷 등)
    Material,      // 2번: 재료 (나무, 철광석 등)
    KeyItem        // 3번: 중요 아이템 (열쇠, 퀘스트 아이템 등)
}
