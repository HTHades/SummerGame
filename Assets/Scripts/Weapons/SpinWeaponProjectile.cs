using UnityEngine;

public class SpinWeaponProjectile : MonoBehaviour
{
    private float damage;
    private float selfRotateSpeed;
    private bool initialized;

   public void Initialize(float damage, float speed)
    {
      this.damage = damage;
      selfRotateSpeed = speed;
      initialized = true;  
    }
    void Update()
    {
        if(!initialized)
        {
            return;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, selfRotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if( !collider.gameObject.CompareTag("Enemy"))
        {
           return;
        }
         IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
    
}
