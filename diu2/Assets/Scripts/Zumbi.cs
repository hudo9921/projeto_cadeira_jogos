using UnityEngine;
using UnityEngine.SceneManagement;

public class Zumbi : MonoBehaviour
{
    public float vida;
    public float initialVelocidade;
    private float velocidade;
    private Rigidbody2D rb;
    private Transform player;

    public float fatorDificuldade=0.1f;
    

    private ScoreUpdater scoreUpdater;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocidade = initialVelocidade;
        player = GameObject.FindWithTag("Player").transform;

        scoreUpdater = FindObjectOfType<ScoreUpdater>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (player.position-transform.position).normalized;

        float gameTime = GameManager.GameTime;

        // Scale velocity based on game time
        velocidade = initialVelocidade + gameTime * fatorDificuldade; 
        //Debug.Log(velocidade);

        if(velocidade>=10.0f){
            rb.velocity = direction * 10.0f;
            Debug.Log("Chegou no cap");
        }
        else{
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
                // Increment the score by 10
                scoreUpdater.IncrementScore(50);
            }
            else
            {
                Debug.LogWarning("ScoreUpdater reference is null. Make sure it is assigned in the inspector or exists in the scene.");
            }
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Restart the game
            SceneManager.LoadScene("JogoOver");
        }
    }
}
