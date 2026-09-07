using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "ChestTile", menuName = "Tiles/ChestTile")]
public class ChestTile : TileBase
{
    [Header("상자 타일 설정")]
    public Sprite chestSprite;
    public ChestData chestData;
    
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.transform = Matrix4x4.identity;
        tileData.flags = TileFlags.LockTransform;
        tileData.colliderType = Tile.ColliderType.Sprite;
        tileData.sprite = chestSprite;
    }
}
