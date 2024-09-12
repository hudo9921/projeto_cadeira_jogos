using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    public void ReturnToMainGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Fase 1");
    }

    public void ReturnMenu()
    {
        
        SceneManager.LoadScene("JogoStart");
        Time.timeScale = 1f;
    }
    public void CloseGame(){
        Application.Quit();
    }
}
