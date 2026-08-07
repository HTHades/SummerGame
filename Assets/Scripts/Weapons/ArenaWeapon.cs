using UnityEngine;

public class ArenaWeapon : Weapon
{
    [SerializeField] private ArenaWeaponPrefab PrefabWeapon;
    private float spawnCounter = 5f;

    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if( spawnCounter > 0f)
        {
            return;
        }
        spawnCounter = CurrentStats.cooldown;
        SpawnArena(CurrentStats);
        
    }
    private void SpawnArena(WeaponStats currentStats)
    {
        ArenaWeaponPrefab spawnedWeapon = Instantiate(PrefabWeapon, transform.position, Quaternion.identity, transform);
        spawnedWeapon.Initialize(currentStats.Damage, currentStats.range, currentStats.duration, currentStats.Speed);
    }
}
