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
}
