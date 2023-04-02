using Mirror;
using System;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    [SerializeField][SyncVar(hook = nameof(OnHealthChanged))] private float health;
    [SerializeField] private float maxHealth;
    private event Action _onDeath;

    public  Action OnDeath { get => _onDeath; set => _onDeath = value; }
    public float Health => health;
    public float MaxHealth => maxHealth;


    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void Sync()
    {
        health = maxHealth;
    }

    public void RestoreHealth(float plusHealth)
    {
        if (health + plusHealth >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += plusHealth;
        }
    }

    private void HealthValidation()
    {
        if (health > 0) return;

        _onDeath?.Invoke();
        Destroy(gameObject.GetComponent<PlayerMovement>());
        Destroy(gameObject.GetComponent<PlayerHealth>());
        Destroy(gameObject.GetComponent<PlayerAnimation>());

    }

    private void OnHealthChanged(float oldHealth, float newHealth)
    {
        
    }

}
