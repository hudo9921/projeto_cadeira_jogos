using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreUpdater : MonoBehaviour
{
    public TMP_Text remainingText;
    public GameObject zombies;

    private int currentScore;

    void Start()
    {
        currentScore = zombies.transform.childCount;
        remainingText.text = "Restam: " + currentScore + " Zumbis";
    }


    public void ReduceScore(int amount = 1)
    {
        currentScore -= amount;
        if (currentScore == 1)
        {
            remainingText.text = "Resta: " + currentScore + " Zumbi";
        }
        else
        {
            remainingText.text = "Restam: " + currentScore + " Zumbis";
        }

        if (currentScore == 0)
        {
            Debug.Log("PROXIMA FASE");

            // TODO call proxima fase
        }
    }
}
