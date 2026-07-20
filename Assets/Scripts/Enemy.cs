using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour , IDamageable
{
    [SerializeField] Transform targetDestination;
    [SerializeField] float speed;
    [SerializeField] float hp =10f;
    [SerializeField] float damage =10f;
    private float nextAttackTime = 0f;
    private float attackCoolDown = 1f;
    private float StopDistance = 0.5f;
    private Rigidbody2D rb2d;

    public void TakeDamage(float amount)
    {
        hp -= amount;
        Debug.Log($"máu hiện tại{hp}");
        if( hp<=0)
        {
           Debug.Log("quái chết");
        }
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        EnemyTarget();
    }
    
    private void EnemyTarget()
    {
      float DistanceToPLayer = Vector2.Distance(transform.position, targetDestination.position);
      if(  DistanceToPLayer >= StopDistance)
        {
            Vector2 Direction = (targetDestination.position - transform.position).normalized;
            rb2d.linearVelocity = Direction*speed;
        }
        else
        {
            rb2d.linearVelocity = Vector2.zero;
            if(Time.time >=  nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCoolDown;
            }
        }
    }
    private void Attack()
    {
     //  Debug.Log(" bằn bằn bằn");
       if( targetDestination != null)
        {
            IDamageable damageable = targetDestination.GetComponent<IDamageable>();
            damageable.TakeDamage(damage);
            Debug.Log($"tấn công vào player với {damage} máu");
        }
    }
}
