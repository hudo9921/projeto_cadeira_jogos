using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehavior : MonoBehaviour
{
    public int healthAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBehavior player = collision.GetComponent<PlayerBehavior>();
            if (player != null && player.life != player.maxHealth)
            {
                player.AddHealth(healthAmount);
                Destroy(gameObject);
            }
        }
    }
}
