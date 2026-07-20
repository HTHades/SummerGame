using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [SerializeField] private float MaxHp = 100f;
    [SerializeField] private float currentHp = 100f;
    [SerializeField] private StatusBar Bar; // Tham chiếu đến thanh máu
    
    private void Start()
    {
        // Cập nhật thanh máu ngay khi game bắt đầu để đảm bảo hiển thị đúng (đầy máu)
        if (Bar != null)
        {
            Bar.SetState(currentHp, MaxHp);
        }
    }

    public void TakeDamage(float amount)
    {
        // Trừ máu và đảm bảo máu không bị âm bằng Mathf.Max
        currentHp -= amount;
        currentHp = Mathf.Max(currentHp, 0); 
        
        Debug.Log($"Máu hiện tại của player: {currentHp}");

        // Cập nhật lại UI thanh máu sau khi nhận sát thương
        if (Bar != null)
        {
            Bar.SetState(currentHp, MaxHp);
        }

        if (currentHp <= 0)
        {
            Debug.Log("Player dead");
            // Thêm logic xử lý khi chết ở đây (ví dụ: Destroy(gameObject), play animation...)
        }
    }
}