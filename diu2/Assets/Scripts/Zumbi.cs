using UnityEngine;
using UnityEngine.SceneManagement;

public class Zumbi : MonoBehaviour
{
    public float vida;
    public float initialVelocidade;
    private float velocidade;
    private Rigidbody2D rb;

    public float fatorDificuldade=0.1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocidade = initialVelocidade;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float gameTime = GameManager.GameTime;

        // Scale velocity based on game time
        velocidade = initialVelocidade + gameTime * fatorDificuldade; 
        Debug.Log(velocidade);

        rb.velocity = transform.right * velocidade;
    }

    public void TakeDamage(float dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Restart the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
