using System;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Vector2Int currentTilePosition = new Vector2Int(0, 0); 
    
    [SerializeField] float tileSize = 20f;
    [SerializeField] Vector2Int playerTilePosition;

    private GameObject[,] terrainTiles;

    [SerializeField] int terrainTile_XCount = 3;
    [SerializeField] int terrainTile_YCount = 3;

    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;

    // 1. Thêm một biến cờ để kiểm tra xem map đã update lần đầu chưa
    private bool isInitialized = false; 

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTile_XCount, terrainTile_YCount];
    }


    private void Update()
    {
        // 1. Dùng Mathf.FloorToInt để tính toạ độ thô, giúp fix lỗi khi nhân vật đi vào vùng toạ độ âm
        playerTilePosition.x = Mathf.FloorToInt((playerTransform.position.x + tileSize / 2f) / tileSize);

        playerTilePosition.y = Mathf.FloorToInt((playerTransform.position.y + tileSize / 2f) /
        tileSize);
        
        // 3. Xử lý update map lần đầu tiên ở frame Update đầu tiên (lúc này các TerrainTile đã được Add xong)
        if (!isInitialized)
        {
            currentTilePosition = playerTilePosition;
            UpdateTileOnScreen();
            isInitialized = true;
        }
        // Xử lý update map khi nhân vật bước sang ô mới
        else if (currentTilePosition != playerTilePosition)
        {
            currentTilePosition = playerTilePosition;
            UpdateTileOnScreen();
        }
    }

    private void UpdateTileOnScreen()
    {
        int povOffsetX = fieldOfVisionWidth / 2;
        int povOffsetY = fieldOfVisionHeight / 2;

        for (int pov_x = -povOffsetX; pov_x <= povOffsetX; pov_x++)
        {
            for (int pov_y = -povOffsetY; pov_y <= povOffsetY; pov_y++)
            {
                int tileToUpdate_x =
                    CalculatePositionOnGrid(
                        playerTilePosition.x + pov_x + povOffsetX,
                        true);

                int tileToUpdate_y =
                    CalculatePositionOnGrid(
                        playerTilePosition.y + pov_y + povOffsetY,
                        false);
                
                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                
                // 4. Thêm kiểm tra null để code an toàn tuyệt đối
                if (tile != null)
                {
                    tile.transform.position = CalculateTilePosition(playerTilePosition.x + pov_x, playerTilePosition.y + pov_y);
                }
                else
                {
                    Debug.LogWarning($"Thiếu TerrainTile tại mảng [{tileToUpdate_x}, {tileToUpdate_y}]. Hãy check lại xem đã gán đủ 9 ô đất chưa!");
                }
            }
        } 
    }

    private Vector3 CalculateTilePosition(int x, int y)
    {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    private int CalculatePositionOnGrid(int currentValue, bool horizontal)
    {
       if (horizontal)
       {
            return ((currentValue % terrainTile_XCount) + terrainTile_XCount) % terrainTile_XCount;
       }
       else
       {
            return ((currentValue % terrainTile_YCount) + terrainTile_YCount) % terrainTile_YCount;
       }
    }

    public void Add(GameObject tilegameObject, Vector2Int tilePosition)
    {
        terrainTiles[tilePosition.x, tilePosition.y] = tilegameObject;
    }
}