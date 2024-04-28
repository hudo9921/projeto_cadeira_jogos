using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed=10;
    public float dashDuration=0.5f;
    private Animator ani;
    private Rigidbody2D rigidbody;
    private Collider2D collider;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    public bool isDashing=false;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }
    void Update()
    {
        Movimento();
        Atirar();
        if(Input.GetKeyDown(KeyCode.K)){
            Dash();
            Debug.Log("Dashando");
        }
    }
    void Movimento()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Movement in x axis
        Vector3 movimento = new Vector3(horizontalInput,verticalInput, 0f);
        transform.position += movimento * Time.deltaTime * moveSpeed;

        // Rotate the character based on input direction
        if (horizontalInput > 0f) // Moving right
        {
            ani.SetTrigger("Andando");
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (horizontalInput < 0f) // Moving left
        {
            ani.SetTrigger("Andando");
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if(verticalInput!=0)
        {
            ani.SetTrigger("Andando");
        }
        else // Not moving horizontally
        {
            ani.SetTrigger("Parado");
        }
    }

    void Atirar()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("tiro");
                Instantiate(bulletPrefab,bulletSpawnPoint.position,bulletSpawnPoint.rotation);
                
                ani.SetTrigger("Atirando");
            }   
    }

    void Dash()
    {
            if (!isDashing)
        {
            collider.enabled = false;

            Vector3 dashDirection = transform.right;

            transform.position += dashDirection * dashSpeed * Time.deltaTime;

            isDashing = true;

            Invoke("EnableCollider", dashDuration);
        }
    }
    
    void EnableCollider()
    {
        collider.enabled=true;

        isDashing=false;
    }
}
