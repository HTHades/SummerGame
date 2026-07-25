using UnityEngine;

public class ArenaWeapon : Weapon
{
    [SerializeField] private GameObject PrefabWeapon;
    private float spawnCounter = 5f;
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if( spawnCounter < 0)
        {
            spawnCounter = Stats[weaponLevel].cooldown;
            GameObject NewPrefabWeapon =Instantiate( PrefabWeapon, transform.position, Quaternion.identity);
            NewPrefabWeapon.transform.parent = transform;
        }
    }
}
