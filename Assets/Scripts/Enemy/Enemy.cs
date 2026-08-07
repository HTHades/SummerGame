
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyAnimation))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyAttack))]
public class Enemy : MonoBehaviour
{
    private Transform player;
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyAnimation enemyAnimation;
    private EnemyHealth enemyHealth;
    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyAnimation = GetComponent<EnemyAnimation>(); 
        enemyHealth = GetComponent<EnemyHealth>();
    }
    private void FixedUpdate()
    {
        if(!player.gameObject.activeSelf)
        {
            enemyMovement.Stop();
            enemyAnimation.SetMovement(Vector2.zero);
            return;
        }
        enemyMovement.MoveToPlayer(player);
        enemyAnimation.SetMovement( enemyMovement.Direction);
        if( enemyMovement.HasReachedPlayer)
        {
            enemyAnimation.SetMovement(Vector2.zero);
            enemyAttack.Attack(player);
        }
    }
    public void SetTarget( Transform target)
    {
        player = target;
        enemyHealth.SetPlayerTransform(target);
    }

}
