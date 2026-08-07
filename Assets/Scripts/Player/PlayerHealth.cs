using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    // máu tối đa, máu hiện tại, nhận damage, chết chưa
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;
    public float MaxHp
    {
        get
        {
            return maxHp;
        }
    }
    public float CurrentHp
    {
        get
        {
            return currentHp;
        }
    }
    void Start()
    {
        currentHp = MaxHp;
        UpdateHealthUI();
    }
    public void TakeDamage( float amount)
    {
       currentHp -= amount;
       currentHp = Mathf.Max(currentHp, 0f);
       UpdateHealthUI();
       if( CurrentHp <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        } 
    }
    public void IncreaseMaxHp(int value)
    {
        if( value < 0)
        {
            return;
        }
        maxHp += value;
        currentHp = MaxHp;
        UpdateHealthUI();
    }
    private void UpdateHealthUI()
    {
        UIController.Instance.UpdateHealthSlider(currentHp, maxHp);
    }
}
