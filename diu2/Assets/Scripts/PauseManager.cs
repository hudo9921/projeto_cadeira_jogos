using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    private float normalVolume = 0.8f; 
    private float pausedVolume = 0.4f; 

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        
        Time.timeScale = 0f;

        
        AudioListener.volume = pausedVolume;

        
        pausePanel.SetActive(true);
    }

    void ResumeGame()
    {
        
        Time.timeScale = 1f;

        
        AudioListener.volume = normalVolume;

        
        pausePanel.SetActive(false);
    }
}
