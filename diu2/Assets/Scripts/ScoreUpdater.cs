using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreUpdater : MonoBehaviour
{
    public TMP_Text remainingText;
    public GameObject zombies;

    private int currentScore;

    public string proximaFase;

    private PlayerBehavior player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>();
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
            PlayerPrefs.SetFloat("Ammo",player.munition);
            PlayerPrefs.SetFloat("Health",player.life);
            SceneManager.LoadScene(proximaFase);

        }
    }
}
