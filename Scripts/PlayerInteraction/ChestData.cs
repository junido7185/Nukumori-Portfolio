using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chest", menuName = "Inventory/Chest")]
public class ChestData : ScriptableObject
{
    [Header("상자 정보")]
    public int chestID; // 상자 고유 번호
    public string chestName; // 상자 이름
    public Sprite chestSprite; // 상자 그림

    [Header("상자 크기")]
    public int width = 5; // 가로 슬롯 개수
    public int height = 3; // 세로 슬롯 개수

    [Header("초기 아이템")]
    public List<ChestItem> initialItems = new List<ChestItem>(); // 처음 열 때 들어있을 아이템들

    [System.Serializable] // Inspector에서 보이게 하기 위해 필요
    public class ChestItem
    {
        public ItemData item; // 어떤 아이템인지
        public int quantity; // 몇 개인지
        [Range(0f, 100f)] // 슬라이더로 0~100 사이 값 설정
        public float dropChance = 100f; // 드롭 확률 (%)
    }
}
