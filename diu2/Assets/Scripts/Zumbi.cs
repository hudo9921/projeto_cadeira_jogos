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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocidade = initialVelocidade;
        player = GameObject.FindWithTag("Player").transform;
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
