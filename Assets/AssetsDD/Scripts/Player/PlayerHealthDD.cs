using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDD : NetworkBehaviour
{
    [SerializeField] private float healthPoints;
    [SerializeField] private float maxHealthPoints = 100f;
    
    [SerializeField] private Image bar;
    
    private void Start()
    {
        bar = FindObjectOfType<HealthBar>().GetComponent<Image>();
        if (isLocalPlayer)
        {
            healthPoints = maxHealthPoints;
            bar.fillAmount = healthPoints/100;
        }
    }
    
    private void ChangeHealthBar(float points)
    {
        bar.fillAmount = points/100;
    }
    
    public void RestoreHealth(float points)
    {
        healthPoints += points;
        ChangeHealthBar(healthPoints);
    }
    
    public void DamageToHealth(float damage)
    {
        healthPoints -= damage;
        ChangeHealthBar(healthPoints);
        if (healthPoints <= 0)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
