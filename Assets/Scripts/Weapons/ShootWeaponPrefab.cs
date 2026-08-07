using Unity.VisualScripting;
using UnityEngine;

public class ShootWeaponPrefab : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 direction;
    private float damage;
    private float duration;
    private bool initialized;
   
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if( initialized == false)
        {
            return;
        }
        duration -= Time.deltaTime;
        if( duration > 0f)
        {
            return;
        }
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime);
        if( transform.localScale.x == 0f)
        {
            Destroy(gameObject);   
            AudioController.Instance.PlaySound(SoundType.ShootWeaponSpawn);
        }
        
    }
    public void Initialize( Vector2 direction, float speed, float projectileDamage, float projectileDuration)
    {
        damage = projectileDamage;
        duration = projectileDuration;
        initialized = true;
        if( direction == Vector2.zero)
        {
            direction = Vector2.down;
        }
        float randomAngle = Random.Range(-0.3f, 3f);
        Vector2 shotDirection = Quaternion.Euler(0f, 0f, randomAngle) * direction;
        rb.linearVelocity = shotDirection * speed;
        AudioController.Instance.PlaySound(SoundType.ShootWeaponDespawn);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponentInParent<IDamageable>();
        if(damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}
