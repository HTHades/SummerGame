using UnityEngine;
using System.Collections.Generic;
public class ArenaWeaponPrefab : MonoBehaviour
{
    public ArenaWeapon Weapon;
    private Vector3 targetSize;
    [SerializeField] private float Timer;
    [SerializeField] private float Counter;
    public List<Enemy> enemiesInRange;
    void Start()
    {
       Weapon = GameObject.Find("AreaWeapon").GetComponent<ArenaWeapon>(); 
       targetSize = Vector3.one * Weapon.range;
       transform.localScale = Vector3.zero;
       Timer = Weapon.duration;
    }

       void Update()
    {
        // grow anf shrink towards target size
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, Time.deltaTime*5f);
        // shrink then Destroy
        Timer -= Time.deltaTime;
        if( Timer < 0)
        {
            targetSize = Vector3.zero;
            if( transform.localScale.x == 0)
            {
                Destroy(gameObject);
            }
        }
        // periodic damage
        Counter -= Time.deltaTime;
        if(Counter <= 0)
        {
           Counter = Weapon.Speed;
           for( int i = 0; i < enemiesInRange.Count ; i++)
            {
                IDamageable damageable= enemiesInRange[i].GetComponent<IDamageable>();
                damageable.TakeDamage(Weapon.Damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if( collision.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.GetComponent<Enemy>());
        }
    }

}
