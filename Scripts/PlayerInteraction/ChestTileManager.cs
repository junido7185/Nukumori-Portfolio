using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class ChestTileManager : MonoBehaviour
{
    [Header("타일맵 설정")]
    public Tilemap chestTilemap;
    public TileBase chestTileBase; // 상자 타일
    
    [Header("상자 설정")]
    public GameObject chestPrefab; // ChestController가 붙은 프리팹
    public ChestData defaultChestData; // 기본 상자 데이터
    
    private Dictionary<Vector3Int, ChestController> chestInstances = new Dictionary<Vector3Int, ChestController>();
    
    void Start()
    {
        CreateChestsFromTilemap();
    }
    
    // 타일맵에서 상자 타일을 찾아 ChestController 생성
    void CreateChestsFromTilemap()
    {
        BoundsInt area = chestTilemap.cellBounds;
        
        for (int x = area.xMin; x < area.xMax; x++)
        {
            for (int y = area.yMin; y < area.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = chestTilemap.GetTile(tilePosition);
                
                // 상자 타일이면 ChestController 생성
                if (tile == chestTileBase)
                {
                    CreateChestAtPosition(tilePosition);
                }
            }
        }
    }
    
    // 특정 위치에 상자 생성
    void CreateChestAtPosition(Vector3Int tilePosition)
    {
        // 월드 좌표로 변환
        Vector3 worldPosition = chestTilemap.CellToWorld(tilePosition);
        
        // 상자 오브젝트 생성
        GameObject chestObj = Instantiate(chestPrefab, worldPosition, Quaternion.identity);
        ChestController chestController = chestObj.GetComponent<ChestController>();
        
        if (chestController != null)
        {
            // 타일 위치 기반으로 고유 ID 생성
            int uniqueID = GenerateUniqueID(tilePosition);
            
            // ChestData 동적 생성 또는 기존 데이터 복사
            ChestData chestData = CreateChestDataForPosition(tilePosition, uniqueID);
            chestController.chestData = chestData;
            
            // Dictionary에 저장
            chestInstances[tilePosition] = chestController;
        }
    }
    
    // 타일 위치 기반 고유 ID 생성
    int GenerateUniqueID(Vector3Int position)
    {
        // 좌표를 기반으로 고유 ID 생성
        // 예: (5, 3) → 5003 또는 해시 함수 사용
        return Mathf.Abs((position.x * 1000 + position.y).GetHashCode()) % 999999;
    }
    
    // 위치별 ChestData 생성
    ChestData CreateChestDataForPosition(Vector3Int position, int uniqueID)
    {
        // 런타임에 ChestData 생성
        ChestData newChestData = ScriptableObject.CreateInstance<ChestData>();
        
        // 기본 데이터 복사
        newChestData.chestID = uniqueID;
        newChestData.chestName = $"상자 ({position.x}, {position.y})";
        newChestData.width = defaultChestData.width;
        newChestData.height = defaultChestData.height;
        newChestData.chestSprite = defaultChestData.chestSprite;
        
        // 초기 아이템도 복사 (필요한 경우)
        newChestData.initialItems = new List<ChestData.ChestItem>(defaultChestData.initialItems);
        
        return newChestData;
    }
    
    // 특정 위치의 상자 가져오기
    public ChestController GetChestAtPosition(Vector3Int position)
    {
        return chestInstances.ContainsKey(position) ? chestInstances[position] : null;
    }
}
