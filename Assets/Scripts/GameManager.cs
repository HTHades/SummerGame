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
       StartCoroutine(ShowGameOverScreen());
    }
    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(1.5f);
         UIController.Instance.GameOverPanel.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void Pause()
    {
        if( UIController.Instance.PausePanel.activeSelf == false && UIController.Instance.GameOverPanel.activeSelf == false)
        {
            UIController.Instance.PausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            UIController.Instance.PausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
      SceneManager.LoadScene("MainMenu");   
    }
}
