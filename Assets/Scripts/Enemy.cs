using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform targetDestination;
    [SerializeField] float speed;
    
    private float nextAttackTime = 0f;
    private float attackCoolDown = 1f;
    private float StopDistance = 0.5f;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        EnemyAttack();
    }
    
    private void EnemyAttack()
    {
      float DistanceToPLayer = Vector2.Distance(transform.position, targetDestination.position);
      if(  DistanceToPLayer >= StopDistance)
        {
            Vector2 Direction = targetDestination.position - transform.position;
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
       Debug.Log(" bằn bằn bằn");
    }
}
