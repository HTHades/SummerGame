using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // làm nhiệm vụ xử lý di chuyển và đẩy lùi
    [SerializeField] private float Speed = 2f;
    [SerializeField] private float stopDistance;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 knockbackDirection;
    private float knockbackForce;
    private float KnockbackTimer;
    public bool HasReachedPlayer{get; private set;}
    public Vector2 Direction
    {
        get
        {
            return direction;
        }
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void MoveToPlayer( Transform player)
    {
        if( KnockbackTimer > 0f)
        {
            KnockbackTimer -= Time.deltaTime;
            rb.linearVelocity = knockbackDirection * knockbackForce;
            return;
        }
        float distance = Vector2.Distance(transform.position, player.position);
        HasReachedPlayer = distance < stopDistance;
        if(HasReachedPlayer)
        {
            Stop();
            return;
        }
        direction = (player.position - transform.position).normalized;
        rb.linearVelocity = Direction * Speed;

    }
    public void Stop()
    {
        direction = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
    }
    public void ApplyKnockBack( Vector2 direction, float force, float duration) // nhận thông tin từ EnemyHealth
    {
       knockbackDirection = direction.normalized;
       knockbackForce = force;
       KnockbackTimer = duration;
    }
}
