using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private PlayerHealth health;

    private void Update()
    {
        bar.fillAmount = health.GetHealth()/100;
    }
}
