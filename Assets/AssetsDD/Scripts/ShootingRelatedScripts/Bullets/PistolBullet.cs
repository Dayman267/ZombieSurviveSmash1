using System;
using UnityEngine;
using Mirror;

public class PistolBullet : NetworkBehaviour
{
    uint owner;
    bool inited;
    Vector2 direction;

    private Rigidbody2D rb;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float rateOfFire = 1f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float reloadSec = 1.5f;

    [SerializeField] private float destroyTime = 2f;

    private void Start()
    {
        if(!isServer) return;
        Invoke("DestroyBullet", destroyTime);
    }
    
    [Server]
    public void Init(uint owner, Vector3 mousePos)
    {
        rb = GetComponent<Rigidbody2D>();
        direction = mousePos - transform.position;
        direction.Normalize();
        this.owner = owner;
        inited = true;
    }

    void Update()
    {
        if(!isServer || !inited) return;
        rb.velocity = direction * speed;
    }

    void DestroyBullet()
    {
        NetworkServer.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.isTrigger && !other.gameObject.CompareTag("SpecialEnemy")) return;
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
            NetworkServer.Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("SpecialEnemy"))
        {
            other.GetComponentInParent<EnemyHealth>().TakeDamage(damage);
            NetworkServer.Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Obstacle"))
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
