using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float cooldown = 1f;
    private float nextAttackTime;

    public void Attack( Transform player)
    {
        if( Time.time < nextAttackTime)
        {
            return;
        }
        IDamageable damageable = player.GetComponent<IDamageable>();
        damageable.TakeDamage(damage);
        nextAttackTime = Time.time + cooldown;
    }
}
