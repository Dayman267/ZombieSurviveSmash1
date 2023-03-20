using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Player") && !collision.gameObject.tag.Equals("Tilemap"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.517f);
            Destroy(gameObject);
        }
        if (collision.gameObject.GetComponent<ZombieHealth>())
        {
            collision.gameObject.GetComponent<ZombieHealth>().TakeDamage(damage);
        }
    }
}
