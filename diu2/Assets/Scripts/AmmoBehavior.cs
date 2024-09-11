using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBehavior : MonoBehaviour
{
    public int ammoAmount = 10;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBehavior player = collision.GetComponent<PlayerBehavior>();
            if (player != null && player.munition != player.maxMunition)
            {
                player.AddAmmo(ammoAmount);
                Destroy(gameObject);
            }
        }
    }
}
