using System;
using UnityEngine;
using Mirror;

public class CommonEnemy : NetworkBehaviour
{
    // private Rigidbody2D rb;
    // private float speed = 5f;
    //
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     rb = GetComponent<Rigidbody2D>();
    //     if (other.gameObject.CompareTag("Player")) Approach(other);
    // }
    //
    // private void Approach(Collider2D collider)
    // {
    //     if (!collider.gameObject.CompareTag("Player")) return;
    //     Vector2 direction = collider.gameObject.transform.position - transform.position;
    //     rb.velocity = direction * speed;
    // }
}
