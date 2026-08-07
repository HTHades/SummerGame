using UnityEngine;

public class SpinWeapon : Weapon
{
    [SerializeField] private SpinWeaponPrefab SpinPrefab;
    private float spawnCounter;
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if( spawnCounter > 0)
        {
           return;
        }
        WeaponStats currentStats = CurrentStats;
        spawnCounter = currentStats.cooldown;
        SpawnWeapons(currentStats);
    }
    private void SpawnWeapons(WeaponStats currentStats)
    {
        int weaponAmount = currentStats.amount;
        if( weaponAmount <= 0)
        {
            return;
        }
        float angleStep = 360f / weaponAmount;
        for( int i = 0; i < weaponAmount; i++)
        {
            float rotationOffset = i * angleStep;
            SpinWeaponPrefab orbit = Instantiate( SpinPrefab, transform.position, Quaternion.identity, transform);
            orbit.initialize( currentStats.Damage, currentStats.duration, currentStats.range, currentStats.Speed, rotationOffset);
            AudioController.Instance.PlaySound(SoundType.SpinWeaponSpawn);
        }
    }
}
