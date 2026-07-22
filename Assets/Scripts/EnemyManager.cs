using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Vector2 spawnArena; // Điểm xuất hiện của kẻ thù
    [SerializeField] private GameObject player; // Tham chiếu đến Transform của player
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab; // Prefab của kẻ thù
        public float spawnTimer; // Thời gian giữa các lần xuất hiện kẻ thù
        public float spawnInterval; // Thời gian giữa các lần xuất hiện kẻ thù
        public int enemiesPerWave; // Số lượng kẻ thù trong mỗi wave
        public int spawnedEnemiesCount; // Số lượng kẻ thù đã xuất hiện trong wave hiện tại
    }
    public List<Wave> waves; // Danh sách các wave
    public int waveIndex = 0; // Chỉ số của wave hiện tại

    private void Update()
    {
        
        waves[waveIndex].spawnTimer -= Time.deltaTime;
        if( waves[waveIndex].spawnTimer <=0)
        {
            waves[waveIndex].spawnTimer = waves[waveIndex].spawnInterval; // Reset spawnTimer sau khi spawn
             SpawnEnemy(); 
        }
        if( waves[waveIndex].spawnedEnemiesCount >= waves[waveIndex].enemiesPerWave)
        {
            waves[waveIndex].spawnedEnemiesCount = 0; // Reset số lượng kẻ thù đã xuất hiện trong wave hiện tại
            if( waves[waveIndex].spawnInterval > 0.3f)
            {
                waves[waveIndex].spawnInterval *= 0.9f; // Giảm spawnTimer để tăng tốc độ xuất hiện kẻ thù
            }
            waveIndex++; // Chuyển sang wave tiếp theo
        }
        if( waveIndex >= waves.Count)
        {
            waveIndex = 0; // Reset waveIndex nếu vượt quá số lượng wave
        }

    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GenerateRandomPosition(); // tạo vị trí ngẫu nhiên trong Arena)

        spawnPosition += player.transform.position; // Cộng thêm vị trí của EnemyManager để spawn trong Arena

        GameObject NewEnemy = Instantiate(waves[waveIndex].enemyPrefab, spawnPosition, Quaternion.identity); // Tạo kẻ thù mới

        NewEnemy.GetComponent<Enemy>().SetTarget(player); // Gán mục tiêu cho kẻ thù mới

        NewEnemy.transform.parent = transform; // Gán EnemyManager làm cha của kẻ thù mới để dễ quản lý

        waves[waveIndex].spawnedEnemiesCount++; // Tăng số lượng kẻ thù đã xuất hiện trong wave hiện tại

    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 Position = new Vector3();
        float randomPos = UnityEngine.Random.value >0.5f ? 1 : -1; // Lấy giá trị ngẫu nhiên từ 0 đến 1
        if( UnityEngine.Random.value >=0.5f)
        {
            Position.x = UnityEngine.Random.Range(-spawnArena.x, spawnArena.x);
            Position.y = randomPos * spawnArena.y; // Lấy giá trị ngẫu nhiên từ -spawnArena.y đến spawnArena.y
        }
        else
        {
            Position.y = UnityEngine.Random.Range(-spawnArena.y, spawnArena.y);
            Position.x = randomPos * spawnArena.x; // Lấy giá trị ngẫu nhiên từ -spawnArena.x đến spawnArena.x
        }

        return Position;
    }
}
