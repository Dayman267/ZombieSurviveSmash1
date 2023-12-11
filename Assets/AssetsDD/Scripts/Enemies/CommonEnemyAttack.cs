using System;
using System.Collections;
using UnityEngine;
using Mirror;

public class CommonEnemyAttack : NetworkBehaviour
{
    [SerializeField] private float damage = 10;

    private bool inTrigger = false;
    [SerializeField] private float waitSecAfterDamaging = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        inTrigger = true;
        StartCoroutine(GiveDamage(other.gameObject.GetComponent<PlayerShield>()));
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        inTrigger = false;
    }

    private IEnumerator GiveDamage(PlayerShield shield)
    {
        while (inTrigger)
        {
            shield.DamageToShield(damage);
            yield return new WaitForSeconds(waitSecAfterDamaging);
        }
    }
}
