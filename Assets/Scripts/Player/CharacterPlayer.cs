using UnityEngine;

public class CharacterPlayer : MonoBehaviour, IDamageable
{
    public static CharacterPlayer Instance;
    [SerializeField] public float PlayerMaxHp = 100f;
    [SerializeField] public float PlayerCurrentHp;

    private bool isImmune = true;
    [SerializeField] private float immunityDuration;
    [SerializeField] private float immunityTimer;
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
    void Update()
    {
        if( immunityTimer >0)
        {
            immunityTimer -= Time.deltaTime;
        }
        else
        {
            isImmune = false;
        }
    }

    public void TakeDamage(float amount)
    {
        if( !isImmune)
        {
            isImmune = true;
            immunityTimer = immunityDuration;
            PlayerCurrentHp -= amount;
            PlayerCurrentHp = Mathf.Max(PlayerCurrentHp, 0); // Đảm bảo máu không âm
            Debug.Log($"Máu hiện tại của player: {PlayerCurrentHp}");
            UIController.Instance.UpdateHealthSlider(); // Cập nhật thanh máu sau khi nhận sát thương
            if (PlayerCurrentHp <= 0)
            {
                Debug.Log("Player dead");
                gameObject.SetActive(false);
                GameManager.Instance.GameOver();
            }
        }
    }
}
