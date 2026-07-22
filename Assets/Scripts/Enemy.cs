using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Enemy : MonoBehaviour , IDamageable
{
    [SerializeField] Transform targetDestination;
    [SerializeField] GameObject deathEffect; // Effect to play when enemy dies
    [SerializeField] float speed;
    [SerializeField] float hp =10f;
    [SerializeField] float damage =10f;
    [SerializeField] private Animator animator;
    [SerializeField] private bool useMoveY;
    [SerializeField] private bool useMoveX;

    private float nextAttackTime = 0f;
    private float attackCoolDown = 1f;
    private float StopDistance = 1f;
    private Rigidbody2D rb2d;
    

    public void TakeDamage(float amount)
    {
        hp -= amount;
        Debug.Log($"máu hiện tại{hp}");
        if( hp<=0)
        {
           Destroy(gameObject);
           Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        targetDestination = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

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
            rb2d.linearVelocity = Direction * speed;
            SetAnimation(Direction);
        }
        else
        {
            rb2d.linearVelocity = Vector2.zero;
            SetAnimation(Vector2.zero);
            if(Time.time >=  nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCoolDown;
            }
        }
    }

    private void SetAnimation(Vector2 Direction)
    {
        if(useMoveX)
        {
            animator.SetFloat("MoveX", Direction.x);
        }

        if(useMoveY)
        {        
            animator.SetFloat("MoveY", Direction.y);
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
    public void SetTarget(GameObject target)
    {
        targetDestination = target.transform;
    }
}
