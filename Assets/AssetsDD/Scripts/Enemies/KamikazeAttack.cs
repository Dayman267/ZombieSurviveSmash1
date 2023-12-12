using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KamikazeAttack : NetworkBehaviour
{
    [SerializeField] private float damage = 20f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Player")) return;
        collider.gameObject.GetComponent<PlayerShield>().DamageToShield(damage);
        GetComponentInParent<KamikazeMovement>().DestroyKamikaze();
    }
}