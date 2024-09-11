using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreUpdater : MonoBehaviour
{
    public TMP_Text scoreText;
    public float scoreIncreaseRate = 1f; 

    private int currentScore = 0;

    void Start()
    {
        
        StartCoroutine(UpdateScore());
    }

    IEnumerator UpdateScore()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(1f / scoreIncreaseRate);
            
            
            IncrementScore();
        }
    }

    
    public void IncrementScore(int amount = 1)
    {
        currentScore += amount;

        
        // scoreText.text = "Score: " + currentScore;
    }
}
