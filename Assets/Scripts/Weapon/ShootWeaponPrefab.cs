using Unity.VisualScripting;
using UnityEngine;

public class ShootWeaponPrefab : MonoBehaviour
{
    private ShootWeapon weapon;
    private Rigidbody2D rb;
    private Vector3 direction;
    private float duration;
    void Start()
    {
        weapon = GameObject.Find("ShootWeapon").GetComponent<ShootWeapon>();
        direction = PlayerMove.Instance.LastMovement;
        duration = weapon.Stats[weapon.weaponLevel].duration;
        rb = GetComponent<Rigidbody2D>();
        float randomAngle = Random.Range(-1f, 1f);
        rb.linearVelocity = new Vector3(direction.x * weapon.Stats[weapon.weaponLevel].Speed + randomAngle, direction.y * weapon.Stats[weapon.weaponLevel].Speed + randomAngle);
    }
    void Update()
    {
        duration -= Time.deltaTime;
        if( duration < 0)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime);
            if( transform.localScale.x == 0f)
            {
              Destroy(gameObject);   
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(weapon.Stats[weapon.weaponLevel].Damage);
        }
    }
}
