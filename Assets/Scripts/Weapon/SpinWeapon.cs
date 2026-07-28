using UnityEngine;

public class SpinWeapon : Weapon
{
    public GameObject prefab;
    private float spawnCounter;
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if( spawnCounter <= 0)
        {
            spawnCounter = Stats[weaponLevel].cooldown;
            for( int i = 0; i < Stats[weaponLevel].amount; i++)
            {
                GameObject spawnedWeapon = Instantiate(prefab, transform.position, Quaternion.identity, transform);
                float rotation = 360f / Stats[weaponLevel].amount * i;
                spawnedWeapon.GetComponent<SpinWeaponPrefab>().SetRotationOffSet(rotation);
            }
        }
        
    }
}
