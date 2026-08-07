using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
   public void NewGame()
    {
        StartCoroutine(DelayLoadScene("Game", 0.5f));
    }
    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator DelayLoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
