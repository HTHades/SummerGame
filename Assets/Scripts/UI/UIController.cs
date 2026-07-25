using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Slider PlayerHealthSlider;
    [SerializeField] private TMP_Text PlayerHealthText;
    [SerializeField] private Slider PlayerExpSlider;
    [SerializeField] private TMP_Text PlayerExpText;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject LevelUpPanel;
    [SerializeField] private TMP_Text timerText;
    public LevelUpButton[] levelUpButtons;
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
    public void UpdateHealthSlider()
    {
        PlayerHealthSlider.maxValue = PlayerController.Instance.PlayerMaxHp;
        PlayerHealthSlider.value = PlayerController.Instance.PlayerCurrentHp;
        PlayerHealthText.text = PlayerHealthSlider.value + " / " + PlayerHealthSlider.maxValue;
    }
    public void UpdateExpSlider()
    {
        PlayerExpSlider.maxValue = PlayerController.Instance.playerLevels[PlayerController.Instance.currentLevel -1];
        PlayerExpSlider.value = PlayerController.Instance.Experience;
        PlayerExpText.text = PlayerExpSlider.value + "/" + PlayerExpSlider.maxValue;
    }

    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    public void LevelUpPanelOpen()
    {
        LevelUpPanel.SetActive(true);
        Time.timeScale = 0f;  
    }
    public void LevelUpPanelClose()
    {
        LevelUpPanel.SetActive(false);
        Time.timeScale = 1f;  
    }
}
