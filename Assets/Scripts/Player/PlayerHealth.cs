using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    public float GetHealth()
    {
        return health;
    }

    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        FindObjectOfType<Canvas>().GetComponentInChildren<HealthBar>().health = this;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
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
}
