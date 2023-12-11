using System;
using UnityEngine;
using Mirror;

public class CommonEnemyMovement : NetworkBehaviour
{
    private Rigidbody2D rb;
    private float speed = 5.3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Player")) return;
        Vector2 direction = collider.gameObject.transform.position - transform.position;
        direction.Normalize();
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90);
        rb.velocity = direction * speed;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Player")) return;
        rb.velocity = Vector2.zero;
    }
}
