using Unity.Mathematics;
using UnityEngine;

public class ShootWeapon : Weapon
{
    [SerializeField] private ShootWeaponPrefab DirectShootPrefab;
    [SerializeField] private PlayerMovement PlayerMovement;
    private float SpawnCounter;
    void Update()
    {
        SpawnCounter -= Time.deltaTime;
        if( SpawnCounter > 0f)
        {
            return;
        }
        SpawnCounter = CurrentStats.cooldown;
        for( int i = 0; i < CurrentStats.amount; i++)
        {
            SpawnProjectile(CurrentStats);
        }
    }
        private void SpawnProjectile( WeaponStats currentStats)
        {
            ShootWeaponPrefab projectile = Instantiate(DirectShootPrefab, transform.position, quaternion.identity);
            projectile.Initialize(
            PlayerMovement.LastMovement, currentStats.Speed, currentStats.Damage, currentStats.duration);
        }
}

