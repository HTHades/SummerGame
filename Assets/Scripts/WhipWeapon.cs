using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class WhipWeapon : MonoBehaviour
{
    [SerializeField] private List<Collider2D> targetsInside = new List<Collider2D>();
    [SerializeField] private float damage =1f;
    private float attackCooldown = 1f;
    private float timer =0f;


    void Update()
    {
        timer -= Time.deltaTime;
        if( timer <= 0)
        {
            Attack();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamageable>() != null)
        {
            targetsInside.Add(collision);
            //Debug.Log("đã thêm");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        targetsInside.Remove(collision);
    }
    private void Attack()
    {
        targetsInside.RemoveAll(target => target == null);

        foreach (Collider2D target in targetsInside)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            Debug.Log($"đã nhận damage: {damage}");
            damageable.TakeDamage(damage);
            
        }
        timer = attackCooldown;
    }
}
