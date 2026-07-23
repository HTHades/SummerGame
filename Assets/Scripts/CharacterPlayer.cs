using UnityEngine;

public class CharacterPlayer : MonoBehaviour, IDamageable
{
    public static CharacterPlayer Instance;
    [SerializeField] public float PlayerMaxHp = 100f;
    [SerializeField] public float PlayerCurrentHp;
    void Awake()
    {
        if( Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        PlayerCurrentHp = PlayerMaxHp; // Khởi tạo máu hiện tại bằng máu tối đa
        UIController.Instance.UpdateHealthSlider(); // Cập nhật thanh máu khi bắt đầu
    }

    public void TakeDamage(float amount)
    {
        // Trừ máu và đảm bảo máu không bị âm bằng Mathf.Max
        PlayerCurrentHp -= amount;
        PlayerCurrentHp = Mathf.Max(PlayerCurrentHp, 0); 
        Debug.Log($"Máu hiện tại của player: {PlayerCurrentHp}");
        UIController.Instance.UpdateHealthSlider();
        if (PlayerCurrentHp <= 0)
        {
            Debug.Log("Player dead");
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
            // Thêm logic xử lý khi chết ở đây (ví dụ: Destroy(gameObject), play animation...)
        }
    }
}