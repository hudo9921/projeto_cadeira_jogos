using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiro : MonoBehaviour
{
    public float velocidade;
    public float dano;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity=transform.right*velocidade;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        Zumbi zumbi = colisao.GetComponent<Zumbi>();

        if(zumbi!=null){
            zumbi.TakeDamage(dano);
        }
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}