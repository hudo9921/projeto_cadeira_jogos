using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // To manage scenes

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 0f;
    public float dashSpeed = 10;
    public float dashDuration = 0.5f;
    public float dashStaminaCost = 10f;
    public float staminaRecoveryRate = 5f;
    public float minStaminaRecoveryRate = 1f;
    public float staminaDecreaseFactor = 0.1f;

    private Animator ani;
    private Rigidbody2D rigidbody;
    private Collider2D collider;
    private GameObject dashBar;
    private float currentStamina;
    private Coroutine staminaRecoveryCoroutine;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    public HealthBarBehavior healthBar;

    public AmmunitionBarBehavior ammunitionBar;

    public WeaponManager weaponManager;

    public bool isDashing = false;

    public AudioSource gunShotSound;

    // Life variable
    public float life;
    public float maxHealth = 100f;


    public float munition;
    public float maxMunition = 45;

    private bool canShoot = true;  // Controle de quando o player pode atirar
    public float pistolCooldown = 0.5f;  // Tempo de recarga para a pistola

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        dashBar = GameObject.FindWithTag("StaminaBar");
        currentStamina = 100f;
        UpdateStaminaBar();
        StartStaminaRecovery();

        life = PlayerPrefs.GetFloat("Health",maxHealth);
        healthBar.SetMaxHealth(maxHealth);
        Debug.Log(life);

        munition = PlayerPrefs.GetFloat("Ammo",maxMunition);
        Debug.Log(munition);
        ammunitionBar.SetMaxAmmunition(maxMunition);
    }

    void Update()
    {
        Movimento();

        if (Input.GetMouseButtonDown(0))
        {
            Atirar();
        }

        if (Input.GetKeyDown(KeyCode.Space) && currentStamina >= dashStaminaCost)
        {
            Dash();
            Debug.Log("Dashando");
        }

        // Check if life is 0 or below, then load lose scene
        if (life <= 0)
        {
            SceneManager.LoadScene("CutSceneLose");
        }
    }

    void Movimento()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        Vector3 newPosition = transform.position + movement * Time.deltaTime * moveSpeed;

        transform.position = newPosition;

        if (horizontalInput > 0f)
        {
            ani.SetTrigger("Andando");
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (horizontalInput < 0f)
        {
            ani.SetTrigger("Andando");
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (verticalInput != 0f)
        {
            ani.SetTrigger("Andando");
        }
        else
        {
            ani.SetTrigger("Parado");
        }
    }

    void Atirar()
    {
        bool hasAmmo = true;
        // Se o jogador não pode atirar, retorna
        if (!canShoot) return;

        // Verifica se a arma atual é uma pistola
        if (weaponManager.currentWeapon.weaponID == WeaponManager.WeaponID.Pistol)
        {
            // Inicia o cooldown para a pistola

            StartCoroutine(PistolCooldownRoutine());
        }
        else
        {

            // Gasta munição para armas que não são pistolas
            if (munition <= 0)
            {
                munition = 0;
                hasAmmo = false;
            }
            else
            {
                munition -= 1;
                Debug.Log(munition);
                ammunitionBar.SetAmmunition(munition);
            }
        }

        // Atira a bala
        if (hasAmmo)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            gunShotSound.Play();
            ani.SetTrigger("Atirando");
        }

    }

    void Dash()
    {
        if (!isDashing && currentStamina >= dashStaminaCost)
        {
            currentStamina -= dashStaminaCost;
            UpdateStaminaBar();

            collider.enabled = false;

            Vector3 dashDirection = -transform.right;
            transform.position += dashDirection * dashSpeed;

            isDashing = true;

            Invoke("EnableCollider", dashDuration);
        }
    }

    void EnableCollider()
    {
        collider.enabled = true;
        isDashing = false;
    }

    void UpdateStaminaBar()
    {
        Vector3 newScale = dashBar.transform.localScale;
        newScale.y = currentStamina / 100f;
        dashBar.transform.localScale = newScale;
    }

    void StartStaminaRecovery()
    {
        if (staminaRecoveryCoroutine != null)
        {
            StopCoroutine(staminaRecoveryCoroutine);
        }
        staminaRecoveryCoroutine = StartCoroutine(RecoverStamina());
    }

    IEnumerator RecoverStamina()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            currentStamina += staminaRecoveryRate;
            currentStamina = Mathf.Clamp(currentStamina, 0f, 100f);
            UpdateStaminaBar();

            staminaRecoveryRate -= staminaDecreaseFactor * Time.deltaTime;
            staminaRecoveryRate = Mathf.Max(staminaRecoveryRate, minStaminaRecoveryRate);
        }
    }

    // Corrotina para controlar o cooldown da pistola
    IEnumerator PistolCooldownRoutine()
    {
        canShoot = false;  // Bloqueia o tiro
        yield return new WaitForSeconds(pistolCooldown);  // Espera o tempo de recarga
        canShoot = true;  // Libera o tiro novamente
    }

    // Call this function to reduce life
    public void TakeDamage(float damage)
    {
        life -= damage;
        healthBar.SetHealth(life);

        Debug.Log("Player Life: " + life);
        if (life <= 0)
        {
            SceneManager.LoadScene("CutSceneLose");
        }
    }

    public void AddAmmo(int amount)
    {
        
        if(munition+amount>maxMunition){
            munition=maxMunition;
        }else{
            munition += amount;
        }

        if (weaponManager.currentWeapon.weaponID != WeaponManager.WeaponID.Pistol)
        {
            ammunitionBar.SetAmmunition(munition);
        }
    }

    public void AddHealth(int health)
    {
        if(life+health>maxHealth){
            life=maxHealth;
        }else{
            life += health;
        }
        
        healthBar.SetHealth(life);
    }
}
