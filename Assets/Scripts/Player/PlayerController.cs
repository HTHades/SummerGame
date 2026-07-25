using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public static PlayerController Instance;
    [SerializeField] public float PlayerMaxHp = 100f;
    [SerializeField] public float PlayerCurrentHp;
    [SerializeField] public int Experience;
    [SerializeField] public int currentLevel;
    [SerializeField] private int maxLevel;
    [SerializeField] public List<int> playerLevels;
    private bool isImmune = true;
    [SerializeField] private float immunityDuration;
    [SerializeField] private float immunityTimer;
    [SerializeField] public Weapon activeWeapon;
    
    
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
        for( int i = playerLevels.Count; i < maxLevel; i++)
        {
            playerLevels.Add(Mathf.CeilToInt(playerLevels[playerLevels.Count-1]*1.1f) + 15);
        }
        PlayerCurrentHp = PlayerMaxHp; // Khởi tạo máu hiện tại bằng máu tối đa
        UIController.Instance.UpdateHealthSlider(); // Cập nhật thanh máu khi bắt đầu
        UIController.Instance.UpdateExpSlider();
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
    public void GetExperience(int experienceToGet)
    {
        Experience += experienceToGet;
        UIController.Instance.UpdateExpSlider();
        if( Experience >= playerLevels[currentLevel-1])
        {
            LevelUp();
        }
    }
    public void LevelUp()
    {
        Experience -= playerLevels[currentLevel-1];
        currentLevel++;
        UIController.Instance.UpdateExpSlider();
        UIController.Instance.levelUpButtons[0].ActivateButton(activeWeapon);
        UIController.Instance.LevelUpPanelOpen();
        
    }
}
