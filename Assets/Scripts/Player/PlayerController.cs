using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public static PlayerController Instance;
    [SerializeField] public float PlayerMaxHp = 100f;
    [SerializeField] public float PlayerCurrentHp;
    public int Experience;
    public int currentLevel;
    public int maxLevel;
    [SerializeField] public List<int> playerLevels;
    [SerializeField] public List<Weapon> ActiveWeapons;
    [SerializeField] public List<Weapon> InactiveWeapons;
    [SerializeField] private List<Weapon> UpgradeableWeapons;
    public List<Weapon> MaxLevelWeapons;
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
            playerLevels.Add(Mathf.CeilToInt(playerLevels[playerLevels.Count-1]*1.9f));
        }
        PlayerCurrentHp = PlayerMaxHp; // Khởi tạo máu hiện tại bằng máu tối đa
        UIController.Instance.UpdateHealthSlider(); // Cập nhật thanh máu khi bắt đầu
        UIController.Instance.UpdateExpSlider();
        AddWeapon(Random.Range(0,InactiveWeapons.Count));
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

       // UIController.Instance.levelUpButtons[0].ActivateButton(activeWeapon);
       UpgradeableWeapons.Clear();
       if( ActiveWeapons.Count > 0)
        {
            UpgradeableWeapons.AddRange(ActiveWeapons);
        }
        if( InactiveWeapons.Count > 0)
        {
            UpgradeableWeapons.AddRange(InactiveWeapons);
        }
        for( int i = 0; i < UIController.Instance.levelUpButtons.Length; i++)
        {
            if( UpgradeableWeapons.ElementAtOrDefault(i) != null)
            {
                UIController.Instance.levelUpButtons[i].ActivateButton(UpgradeableWeapons[i]);
                UIController.Instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.Instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }
        UIController.Instance.LevelUpPanelOpen();
        
    }
    public void AddWeapon(int index)
    {
        ActiveWeapons.Add(InactiveWeapons[index]);
        InactiveWeapons[index].gameObject.SetActive(true);
        InactiveWeapons.RemoveAt(index);
    }
    public void ActiveWeapon( Weapon weapon)
    {
        weapon.gameObject.SetActive(true);
        ActiveWeapons.Add(weapon);
        InactiveWeapons.Remove(weapon);
    }
    public void IncreaseMaxHp(int value)
    {
        PlayerMaxHp += value;
        PlayerCurrentHp = PlayerMaxHp;
        UIController.Instance.UpdateHealthSlider();
        UIController.Instance.LevelUpPanelClose();
        AudioController.Instance.PlaySound(AudioController.Instance.SelectUpgrade);
    }
}
