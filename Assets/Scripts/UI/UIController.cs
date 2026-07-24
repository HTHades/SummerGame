using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Slider PlayerHealthSlider;
    [SerializeField] private TMP_Text PlayerHealthText;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
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
    public void UpdateHealthSlider()
    {
        PlayerHealthSlider.maxValue = CharacterPlayer.Instance.PlayerMaxHp;
        PlayerHealthSlider.value = CharacterPlayer.Instance.PlayerCurrentHp;
        PlayerHealthText.text = PlayerHealthSlider.value + " / " + PlayerHealthSlider.maxValue;
    }

    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
