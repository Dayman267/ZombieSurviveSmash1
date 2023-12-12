using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KamikazeMovement : NetworkBehaviour
{
    [SerializeField] private float speed = 6f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Player")) return;
        Vector2 direction = collider.gameObject.transform.position - transform.position;
        direction.Normalize();
        rb.velocity = direction * speed;
    }

    public void DestroyKamikaze()
    {
        NetworkServer.Destroy(gameObject);
    }
}
