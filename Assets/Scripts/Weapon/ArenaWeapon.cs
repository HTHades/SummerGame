using UnityEngine;

public class ArenaWeapon : MonoBehaviour
{
    [SerializeField] private GameObject PrefabWeapon;
    private float spawnCounter = 5f;
    public float cooldown = 5f;
    public float duration = 3f;
    [SerializeField] public float Damage = 5f;
    public float range = 0.7f;
    public float Speed = 0.5f;
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if( spawnCounter < 0)
        {
            spawnCounter = cooldown;
            GameObject NewPrefabWeapon =Instantiate( PrefabWeapon, transform.position, Quaternion.identity);
            NewPrefabWeapon.transform.parent = transform;
        }
    }
}
