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
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject LevelUpPanel;
    public bool IsGameOverPanelOpen
    {
        get
        {
            return GameOverPanel.activeSelf;
        }
    }
    public bool IsPausePanelOpen
    {
        get
        {
            return PausePanel.activeSelf;
        }
    }
    public bool IsLevelUpPanelOpen
    {
        get
        {
          return LevelUpPanel.activeSelf;   
        }
    }
    [SerializeField] private TMP_Text timerText;
    
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
    public void UpdateHealthSlider(float currentHp, float maxHp)
    {
        PlayerHealthSlider.maxValue = maxHp;
        PlayerHealthSlider.value = currentHp;
        PlayerHealthText.text = currentHp + " / " + maxHp;
    }
    public void UpdateExpSlider( int currentExperience, int requiredExperience)
    {
        PlayerExpSlider.maxValue = requiredExperience;
        PlayerExpSlider.value = currentExperience;
        PlayerExpText.text = currentExperience + "/" + requiredExperience;
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
    }
    public void LevelUpPanelClose()
    {
        LevelUpPanel.SetActive(false);
    }
    public void SetPausePanel( bool isOpen)
    {
        PausePanel.SetActive(isOpen);
    }
    public void SetGameOverPanel(bool isOpen)
    {
        GameOverPanel.SetActive(isOpen);
    }

}
