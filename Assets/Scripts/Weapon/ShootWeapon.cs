using Unity.Mathematics;
using UnityEngine;

public class ShootWeapon : Weapon
{
    [SerializeField] private GameObject DirectShootPrefab;
    private float SpawnCounter;
    void Update()
    {
        SpawnCounter -= Time.deltaTime;
        if( SpawnCounter <= 0)
        {
            SpawnCounter = Stats[weaponLevel].cooldown;
            for( int i =0; i < Stats[weaponLevel].amount; i++)
            {
                Instantiate(DirectShootPrefab, transform.position, Quaternion.identity, transform );
            }
        }
    }
}
