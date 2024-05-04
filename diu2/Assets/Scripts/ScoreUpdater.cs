using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreUpdater : MonoBehaviour
{
    public TMP_Text scoreText;
    public float scoreIncreaseRate = 1f; // Score increase rate per second

    private int currentScore = 0;

    void Start()
    {
        // Start the coroutine to continuously update the score
        StartCoroutine(UpdateScore());
    }

    IEnumerator UpdateScore()
    {
        while (true)
        {
            // Wait for the specified time before incrementing the score again
            yield return new WaitForSeconds(1f / scoreIncreaseRate);
            
            // Increase the score by the score increase rate
            IncrementScore();
        }
    }

    // Method to increment the score by a specified amount
    public void IncrementScore(int amount = 1)
    {
        currentScore += amount;

        // Update the TextMeshProUGUI component with the current score
        scoreText.text = "Score: " + currentScore;
    }
}
