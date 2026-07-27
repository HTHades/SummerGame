using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Enemy : MonoBehaviour , IDamageable
{
    [SerializeField] Transform targetDestination;
    [SerializeField] GameObject deathEffect; // Effect to play when enemy dies
    [SerializeField] float speed;
    [SerializeField] float Health;
    [SerializeField] float damage;
    [SerializeField] private Animator animator;
    [SerializeField] private bool useMoveY;
    [SerializeField] private bool useMoveX;

    private float nextAttackTime = 0f;
    private float attackCoolDown = 1f;
    private float StopDistance = 0.5f;
    private Rigidbody2D rb2d;
    private float DistanceToPLayer;
    [SerializeField] private int ExperienceToGive;
    [SerializeField] private float pushTime;
    private float pushCounter;
    
    public void TakeDamage(float amount)
    {
        Health -= amount;
        pushCounter = pushTime;
        DamageNumberController.Instance.CreateNumber(amount, transform.position);
        Debug.Log($"máu hiện tại{Health}");
        if( Health<=0)
        {
            PlayerController.Instance.GetExperience(ExperienceToGive);
            Destroy(gameObject);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            AudioController.Instance.PlayEnemyDieSounnd(AudioController.Instance.EnemyDie);
        }
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    private void FixedUpdate()
    {
        EnemyTarget();
    }
    
    private void EnemyTarget()
    {

      if( targetDestination.gameObject.activeSelf)
        {
             DistanceToPLayer = Vector2.Distance(transform.position, targetDestination.position);
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
            // push back
            if( pushCounter >0)
            {
                pushCounter -= Time.deltaTime;
                if(speed >=0)
                {
                    speed = -speed ;
                }
                if(pushCounter <=0)
                {
                    speed = Mathf.Abs(speed);
                }
            }
        }
        else
        {
            rb2d.linearVelocity = Vector2.zero;
            SetAnimation(Vector2.zero);
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
