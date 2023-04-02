using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    public PlayerHealth health;

    public void SetPlayerHealth(PlayerHealth playerHealth )
    {
        health = playerHealth;
    }
    private void Update()
    {   
        if ( health != null )
        bar.fillAmount = health.Health/health.MaxHealth;
    }

}
