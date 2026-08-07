using UnityEngine;
using System.Collections.Generic;
public class ArenaWeaponPrefab : MonoBehaviour
{
    [SerializeField] private float resizeSpeed;
    [SerializeField] private float damageCounter;
    private Vector3 targetSize;
    private float damage;
    private float duration;
    private float damageInterval;
    private bool initialized = false;
    private bool isDespawning = false;

    public List<IDamageable> enemiesInRange = new List<IDamageable>();

    public void Initialize( float damageAmount, float range, float duration, float interval)
    {
       damage = damageAmount;
       this.duration = duration;
       damageInterval = interval;
       damageCounter = 0f;
       transform.localScale = Vector3.zero;
       targetSize = Vector3.one * range;
       initialized = true;
       AudioController.Instance.PlaySound(SoundType.ArenaWeaponSpawn);
    }

       void Update()
    {
       if(!initialized)
       {
            return;
       }
       
       Resize();

       UpdateDuration();

       if(!isDespawning)
       {
          UpdateDamage();
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       IDamageable damageable = collision.GetComponent<IDamageable>();
       if( damageable == null)
        {
          return;  
        }
        enemiesInRange.Add(damageable);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if( damageable == null)
        {
          return;  
        }
        enemiesInRange.Remove(damageable);
    }
        private void Resize()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, Time.deltaTime * resizeSpeed);
        if( isDespawning && transform.localScale.x == 0)
        {
            AudioController.Instance.PlaySound(SoundType.ArenaWeaponDespawn);
            Destroy(gameObject);
        }
    }
    private void UpdateDuration()
    {
        duration -= Time.deltaTime;
        if( duration > 0f)
        {
         return;
        }
        isDespawning = true;
        targetSize = Vector3.zero;
    }
    private void UpdateDamage()
    {
        damageCounter -= Time.deltaTime;
        if( damageCounter > 0f)
        {
            return;
        }
        damageCounter = damageInterval;
        DamageAllTargets();
    }
    private void DamageAllTargets()
    {
        for( int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
           IDamageable Target = enemiesInRange[i];
           if( IsMissing(Target))
           {
                enemiesInRange.RemoveAt(i);
                continue;
           }
           Target.TakeDamage(damage);
        } 
    }
    private bool IsMissing ( IDamageable target)
    {
        if( target == null)
        {
            return true;
        }
        if( target is UnityEngine.Object unityObject) // dùng để kiểm tra object từng tồn tại nhưng đã bị destroy
        {
            return unityObject == null;
        }
        return false;
    }

}
