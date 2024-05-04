using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    public void ReturnToMainGame()
    {
        SceneManager.LoadScene("JogoMain");
    }

    public void CloseGame()
    {
        SceneManager.LoadScene("JogoStart");
    }
}
