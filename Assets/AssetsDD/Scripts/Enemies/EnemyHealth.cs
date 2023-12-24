using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class EnemyHealth : NetworkBehaviour
{
    [SerializeField] private int health;

    [Server]
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health<= 0) NetworkServer.Destroy(gameObject);
    }
}
