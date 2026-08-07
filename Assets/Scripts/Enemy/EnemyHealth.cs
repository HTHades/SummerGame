using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    //  quản lý nhận damage, hiện damage, Die
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float knockbackForce = 2f;
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private int experrienceToGive = 10;
    [SerializeField] private PlayerExperience playerExperience;
    private bool isDead;
    private Transform player;

    [SerializeField] private GameObject deathEffect;
    private EnemyMovement enemyMovement;
    void Awake()
    {
        currentHealth = maxHealth;
        enemyMovement = GetComponent<EnemyMovement>();
    }
    public void TakeDamage(float amount)
    {
        if(isDead)
        {
            return;
        }
        currentHealth -= amount;
        ShowDamageNumber(amount);
        RequestKnockBack();
        if( currentHealth <=0)
        {
            Die();
        }
    }
    public void ShowDamageNumber(float amount)
    {
        DamageNumberController.Instance.CreateNumber( amount, transform.position);
    }
    private void Die()
    {
        if(isDead)
        {
            return;
        }
        isDead = true;
        playerExperience.AddExperience(experrienceToGive);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        AudioController.Instance.PlaySound(SoundType.EnemyDeath);
        Destroy(gameObject);
    }
    private void RequestKnockBack() // tính hướng đẩy, lực, thời gian 
    {
        Vector2 Direction = transform.position - player.position;
        enemyMovement.ApplyKnockBack( Direction, knockbackForce, knockbackDuration);
    }
    public void SetPlayerTransform(Transform playerTransform)
    {
        player = playerTransform;
        playerExperience =playerTransform.GetComponent<PlayerExperience>();
    }
    
}
