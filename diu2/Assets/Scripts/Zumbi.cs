using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Zumbi : MonoBehaviour
{
    public float vida;
    public float initialVelocidade;
    private float velocidade;
    private Rigidbody2D rb;
    private Transform player;

    public float fatorDificuldade = 0.1f;
    public AudioClip[] zombieSounds;  // Array of audio clips
    public AudioSource audioSource;   // Reference to the AudioSource component
    public float minSoundInterval = 10f;  // Minimum interval between sounds
    public float maxSoundInterval = 15f;  // Maximum interval between sounds
    public float damage = 20f;            // Damage dealt to the player
    public float damageInterval = 1f;     // Interval between each damage tick (in seconds)

    private ScoreUpdater scoreUpdater;
    private bool isAlive = true;      // Flag to check if the zombie is alive
    private bool isCollidingWithPlayer = false; // To track if zombie is colliding with the player
    private Coroutine damageCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocidade = initialVelocidade;
        player = GameObject.FindWithTag("Player").transform;
        scoreUpdater = FindObjectOfType<ScoreUpdater>();

        if (isAlive && zombieSounds.Length > 0)
        {
            StartCoroutine(PlayRandomSoundAtIntervals());
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float gameTime = GameManager.GameTime;

        velocidade = initialVelocidade + gameTime * fatorDificuldade;

        if (velocidade >= 10.0f)
        {
            rb.velocity = direction * 10.0f;
            Debug.Log("Chegou no cap");
        }
        else
        {
            rb.velocity = direction * velocidade;
        }
    }

    public void TakeDamage(float dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            if (scoreUpdater != null)
            {
                scoreUpdater.IncrementScore(50);
            }
            else
            {
                Debug.LogWarning("ScoreUpdater reference is null. Make sure it is assigned in the inspector or exists in the scene.");
            }
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Start dealing damage if the player is in range
            if (!isCollidingWithPlayer)
            {
                isCollidingWithPlayer = true;
                damageCoroutine = StartCoroutine(DamagePlayerOverTime(other.GetComponent<PlayerBehavior>()));
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop dealing damage when the player is out of range
            if (isCollidingWithPlayer)
            {
                isCollidingWithPlayer = false;
                if (damageCoroutine != null)
                {
                    StopCoroutine(damageCoroutine);
                }
            }
        }
    }

    private IEnumerator DamagePlayerOverTime(PlayerBehavior playerBehavior)
    {
        while (isCollidingWithPlayer)
        {
            if (playerBehavior != null)
            {
                playerBehavior.TakeDamage(damage); // Deal damage
            }
            yield return new WaitForSeconds(damageInterval); // Wait for next damage tick
        }
    }

    private IEnumerator PlayRandomSoundAtIntervals()
    {
        while (isAlive)
        {
            PlayRandomSound();
            float randomInterval = Random.Range(minSoundInterval, maxSoundInterval);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    private void PlayRandomSound()
    {
        if (isAlive && zombieSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, zombieSounds.Length);
            audioSource.clip = zombieSounds[randomIndex];
            audioSource.Play();
        }
    }

    private void Die()
    {
        isAlive = false;
        StopAllCoroutines(); // Stop any running coroutines, including sound playback
        Destroy(gameObject);
    }
}
