using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;

    private float normalVolume = 0.8f; // Volume when the game is playing
    private float pausedVolume = 0.4f; // Volume when the game is paused

    void Update()
    {
        // Check for pause input (e.g., pressing the "Pause" button)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        // Toggle pause state
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
        // Pause the game by setting time scale to 0
        Time.timeScale = 0f;

        // Set the audio volume for the paused state
        AudioListener.volume = pausedVolume;

        // Show the pause panel
        pausePanel.SetActive(true);
    }

    void ResumeGame()
    {
        // Unpause the game by setting time scale back to 1
        Time.timeScale = 1f;

        // Set the audio volume for the normal playing state
        AudioListener.volume = normalVolume;

        // Hide the pause panel
        pausePanel.SetActive(false);
    }
}
