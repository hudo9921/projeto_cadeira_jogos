using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossBehavior : MonoBehaviour
{
    public GameObject player;
    public float safeDistance = 10f; // Distância segura que o boss mantém do jogador
    public float moveSpeed = 3f; // Velocidade de movimento do boss
    public float verticalMoveRange = 2f; // Amplitude do movimento vertical
    public float damageRange = 5f; // Distância em que o boss pode causar dano
    public GameObject laserCollider; // Objeto vazio com Collider2D para o laser
    public float laserDuration = 0.5f; // Duração em segundos que o laser fica ativo
    public Animator bossAnimator; // Animator do boss para a animação de disparo
    public float fireRate = 2f; // Tempo entre os ataques
    public int bossHealth = 3200; // Vida do boss
    public int laserDamage = 10; // Dano causado pelo laser
    public float damageInterval = 1f; // Intervalo entre os danos causados pelo laser

    private float fireCooldown = 0f;
    private bool isFacingRight = true;
    private bool isAttacking = false;
    private bool isTakingDamage = false;
    private Coroutine damageCoroutine; // Para armazenar a corrotina de dano ao jogador

    public HealthBarBehavior healthBar;

    void Start(){

        healthBar.SetMaxHealth(bossHealth);
    }

    void Update()
    {
        if (bossHealth <= 0)
        {
            Die();
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        FlipTowardsPlayer();

        if (!isTakingDamage && !isAttacking)
        {
            if (distanceToPlayer > safeDistance)
            {
                MoveTowardsPlayer();
            }
            else if (distanceToPlayer < damageRange)
            {
                AttackPlayer();
            }
            else
            {
                Idle();
            }
        }

        // Reduz o cooldown do ataque
        fireCooldown -= Time.deltaTime;
    }

    void FlipTowardsPlayer()
    {
        // Verifica se o jogador está à esquerda ou à direita
        if (player.transform.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (player.transform.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // Inverte a direção do boss mudando a escala em X
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // Inverte o valor de X
        transform.localScale = theScale;
    }

    void MoveTowardsPlayer()
    {
        // Ativa a animação de "Andando"
        bossAnimator.SetTrigger("Andando");

        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Movimentação vertical aleatória
        float verticalMovement = Mathf.Sin(Time.time) * verticalMoveRange;

        // Calcula a nova posição Y com o movimento vertical
        float newYPosition = transform.position.y + verticalMovement * Time.deltaTime * moveSpeed;

        // Garante que o chefe não se afaste mais que 5 unidades do jogador no eixo Y
        if (Mathf.Abs(newYPosition - player.transform.position.y) > 5f)
        {
            if (newYPosition > player.transform.position.y)
            {
                newYPosition = player.transform.position.y + 5f;
            }
            else
            {
                newYPosition = player.transform.position.y - 5f;
            }
        }

        // Movimentação final com limite no eixo Y
        Vector2 movement = new Vector2(direction.x, newYPosition - transform.position.y);
        transform.position += (Vector3)(movement * moveSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        if (fireCooldown <= 0f)
        {
            isAttacking = true;
            // Ativa a animação de disparo de laser
            bossAnimator.SetTrigger("Atirando");

            // Liga o collider do laser para detectar colisões
            StartCoroutine(ActivateLaserCollider());

            fireCooldown = fireRate;
        }
    }

    IEnumerator ActivateLaserCollider()
    {
        yield return new WaitForSeconds(1);
        laserCollider.SetActive(true); // Liga o objeto vazio com o Collider2D
        yield return new WaitForSeconds(laserDuration); // Mantém ativo por uma certa duração
        laserCollider.SetActive(false); // Desliga o collider após o tempo definido
        isAttacking = false;
    }

    void Idle()
    {
        // Ativa a animação de "Parado"
        bossAnimator.SetTrigger("Parado");
    }

    public void TakeDamage(int damage)
    {
        if (bossHealth > 0)
        {
            bossHealth -= damage;
            healthBar.SetHealth(bossHealth);

            // Ativa a animação de "Apanhando"
            bossAnimator.SetTrigger("Apanhando");

            StartCoroutine(TakingDamage());
        }
    }

    IEnumerator TakingDamage()
    {
        isTakingDamage = true;
        yield return new WaitForSeconds(0.5f); // Duração da animação de "Apanhando"
        isTakingDamage = false;
    }

    void Die()
    {
        // Ativa a animação de "Morrendo"
        bossAnimator.SetTrigger("Morrendo");

        // Desativa o boss após a animação de morte
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(1.5f); // Tempo para a animação de "Morrendo"
        Destroy(gameObject); // Remove o boss da cena
        SceneManager.LoadScene("CutSceneWin");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Inicia o processo de dano contínuo ao jogador
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DealDamageOverTime(collision));
            }
        }

        // Verifica se o boss foi atingido por um projétil
        if (collision.CompareTag("Tiro"))
        {
            // Aplica dano ao boss
            TakeDamage(20); // Ajuste o valor do dano conforme necessário
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Para de causar dano se o jogador sair da área do laser
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    IEnumerator DealDamageOverTime(Collider2D player)
    {
        while (true)
        {
            Debug.Log("Jogador atingido pelo laser! Causar dano aqui.");
            // Aqui você pode chamar o método para causar dano ao jogador
            player.GetComponent<PlayerBehavior>().TakeDamage(34);

            yield return new WaitForSeconds(damageInterval); // Aplica dano a cada intervalo de tempo
        }
    }
}
