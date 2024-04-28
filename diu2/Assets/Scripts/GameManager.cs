using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private float gameStartTime;

    public static float GameTime => instance ? Time.time - instance.gameStartTime : 0f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        gameStartTime = Time.time;
    }
}
