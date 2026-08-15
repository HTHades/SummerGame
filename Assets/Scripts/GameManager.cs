using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float gameTime;
    private bool gameActive;

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
        gameActive = true;
    }
    void Update()
    {
        if( gameActive)
        {
            gameTime+= Time.deltaTime;
            UIController.Instance.UpdateTimer(gameTime);
            if( Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Pause();
            }
        }
    }

    public void GameOver()
    {
        gameActive = false;
        AudioController.Instance.PlaySound(SoundType.GameOver);
        StartCoroutine(ShowGameOverScreen());
    }
   
    public void Restart()
    {
        StartCoroutine(LoadSceneDelay("Game", 1.5f));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Pause()
    {
        if (UIController.Instance.IsGameOverPanelOpen)
        {
            return;
        }
        bool isOpening = !UIController.Instance.IsPausePanelOpen;
        UIController.Instance.SetPausePanel(isOpening);
        RefreshTimeScale();
    }

    public void GoToMainMenu()
    {
        Debug.Log(" đã nhấn");
       StartCoroutine(LoadSceneDelay("MainMenu", 0.5f));   
    }
    public void GoToMainMenuFromPause()
    {
        StartCoroutine(LoadSceneDelay("MainMenu", 0.5f));
    }
     IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(1.5f);
        UIController.Instance.SetGameOverPanel(true);
        RefreshTimeScale();
        
    }
    IEnumerator LoadSceneDelay( string sceneName, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f; // Reset time scale before loading the scene
        SceneManager.LoadScene(sceneName);
    }
    public void RefreshTimeScale()
    {
        bool shouldPause = UIController.Instance.IsPausePanelOpen || 
                     UIController.Instance.IsLevelUpPanelOpen ||
                     UIController.Instance.IsGameOverPanelOpen;
        Time.timeScale = shouldPause ? 0f : 1f;
    }
   
}
