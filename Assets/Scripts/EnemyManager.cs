using System;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Prefab của kẻ thù
    [SerializeField] private Vector2 spawnArena; // Điểm xuất hiện của kẻ thù
    [SerializeField] private float spawnTime = 5f; // Thời gian giữa các lần xuất hiện kẻ thù
    [SerializeField] private GameObject player; // Tham chiếu đến Transform của player
    private float timer; // Bộ đếm thời gian
    private void Update()
    {
        timer -= Time.deltaTime; // Giảm timer theo thời gian thực
        if(timer<=0)
        {
            SpawnEnemy(); // Gọi hàm spawn kẻ thù
            timer = spawnTime; // Reset timer sau khi spawn
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GenerateRandomPosition(); // tạo vị trí ngẫu nhiên trong Arena)
        spawnPosition += player.transform.position; // Cộng thêm vị trí của EnemyManager để spawn trong Arena
        GameObject NewEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); // Tạo kẻ thù mới
        //NewEnemy.GetComponent<Enemy>().SetTarget(player); // Gán mục tiêu cho kẻ thù mới
        NewEnemy.transform.parent = transform; // Gán EnemyManager làm cha của kẻ thù mới để dễ quản lý

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
