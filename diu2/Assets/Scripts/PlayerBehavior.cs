using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10;
    public float dashDuration = 0.5f;
    public float dashStaminaCost = 10f;
    public float staminaRecoveryRate = 5f;
    public float minStaminaRecoveryRate = 1f;
    public float staminaDecreaseFactor = 0.1f;

    //limitadores
    public GameObject minXObject;
    public GameObject maxXObject;
    public GameObject minYObject;
    public GameObject maxYObject;


    private Animator ani;
    private Rigidbody2D rigidbody;
    private Collider2D collider;
    private GameObject dashBar;
    private float currentStamina;
    private Coroutine staminaRecoveryCoroutine;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    public bool isDashing = false;

    public AudioSource gunShotSound;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        dashBar = GameObject.FindWithTag("StaminaBar");
        currentStamina = 100f;
        UpdateStaminaBar();
        StartStaminaRecovery();

    }
    void Update()
    {
        Movimento();
        Atirar();
        if (Input.GetKeyDown(KeyCode.Space) && currentStamina >= dashStaminaCost)
        {
            Dash();
            Debug.Log("Dashando");
        }
    }
    void Movimento()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        Vector3 newPosition = transform.position + movement * Time.deltaTime * moveSpeed;


        float minX = minXObject.transform.position.x;
        float maxX = maxXObject.transform.position.x;
        float minY = minYObject.transform.position.y;
        float maxY = maxYObject.transform.position.y;


        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);


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
        if (Input.GetMouseButtonDown(0))
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
}
