using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    //  quản lý nhận damage, hiện damage, Die
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float knockbackForce = 2f;
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private int experrienceToGive = 10;
    private bool isDead;

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
        ApplyKnockBack();
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
        PlayerController.Instance.GetExperience(experrienceToGive);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        AudioController.Instance.PlayEnemyDieSounnd(AudioController.Instance.EnemyDie);
        Destroy(gameObject);
    }
    private void ApplyKnockBack()
    {
        Vector2 Direction = transform.position - PlayerController.Instance.transform.position;
        enemyMovement.ApplyKnockBack( Direction, knockbackForce, knockbackDuration);
    }
    
}
