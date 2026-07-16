using System;
using UnityEngine;

public class WorldSrolling : MonoBehaviour
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

    private void Awake()
    {
        terrainTiles = new GameObject[terrainTile_XCount, terrainTile_YCount];
    }
    private void Start()
    {
        UpdateTileOnScreen();
    }

    private void Update()
    {
        // 1. Dùng Mathf.FloorToInt để tính toạ độ thô, giúp fix lỗi khi nhân vật đi vào vùng toạ độ âm
        playerTilePosition.x = Mathf.FloorToInt(playerTransform.position.x / tileSize);
        playerTilePosition.y = Mathf.FloorToInt(playerTransform.position.y / tileSize);
        
        // 2. Nếu nhân vật bước sang ô mới thì mới update map
        if (currentTilePosition != playerTilePosition)
        {
            currentTilePosition = playerTilePosition;
            
            // Đã xóa biến onTileGridPlayerPosition thừa thãi ở đây
            
            UpdateTileOnScreen();
        }
    }

    private void UpdateTileOnScreen()
    {
        // Tính toán để quét xung quanh nhân vật (Ví dụ Width=3 -> povOffsetX = 1)
        int povOffsetX = fieldOfVisionWidth / 2;
        int povOffsetY = fieldOfVisionHeight / 2;

        // Vòng lặp quét từ -1 đến 1 (căn giữa nhân vật)
        for (int pov_x = -povOffsetX; pov_x <= povOffsetX; pov_x++)
        {
            for (int pov_y = -povOffsetY; pov_y <= povOffsetY; pov_y++)
            {
                // Tìm xem ô đất nào trong mảng 3x3 cần được lôi ra
                int tileToUpdate_x = CalculatePositionOnGrid(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnGrid(playerTilePosition.y + pov_y, false);
                
                // Lấy ô đất đó ra
                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                
                // Di chuyển nó đến vị trí thực tế trên Scene
                tile.transform.position = CalculateTilePosition(playerTilePosition.x + pov_x, playerTilePosition.y + pov_y);
            }
        } 
    }

    private Vector3 CalculateTilePosition(int x, int y)
    {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    private int CalculatePositionOnGrid(int currentValue, bool horizontal)
    {
       // Xử lý index xoay vòng (Wrap-around), công thức này của bạn xử lý số âm rất chuẩn!
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
