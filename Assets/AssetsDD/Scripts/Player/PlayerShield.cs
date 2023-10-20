using System.Collections;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;
using UnityEngine.UI;

public class PlayerShield : NetworkBehaviour
{
    [SerializeField] private float shieldPoints;
    [SerializeField] private float maxShieldPoints = 100f;
    
    private Image bar;

    private PlayerHealthDD playerHealth;
    
    private bool isDamagedRecently = false;
    [SerializeField] private float waitSecAfterDamage = 2f;
    [SerializeField] private float waitSecBetweenRestoring = 0.1f;
    
    private void Start()
    {
        if (isLocalPlayer)
        {
            playerHealth = GetComponent<PlayerHealthDD>();
            bar = FindObjectOfType<ShieldBar>().GetComponent<Image>();
            shieldPoints = maxShieldPoints;
            bar.fillAmount = shieldPoints/100;
            StartCoroutine(RecoveryByTime());
        }
    }
    
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                DamageToShield(20f);
            }
    
            if (Input.GetKeyDown(KeyCode.F))
            {
                RestoreShield(20f);
            }
        }
    }

    private void ChangeShieldBar(float shieldPoints)
    {
        bar.fillAmount = shieldPoints/100;
    }
    
    private void RestoreShield(float points)
    {
        if (shieldPoints < 100)
        {
            shieldPoints += points;
            ChangeShieldBar(shieldPoints);
        }
    }
    
    private void DamageToShield(float damage)
    {
        isDamagedRecently = true;
        if (shieldPoints - damage >= 0)
        {
            shieldPoints -= damage;
            ChangeShieldBar(shieldPoints);
        }
        else if (shieldPoints > 0 && shieldPoints - damage < 0)
        {
            float remainder = damage - shieldPoints;
            shieldPoints = 0;
            ChangeShieldBar(shieldPoints);
            playerHealth.DamageToHealth(remainder);
        }
        else
        {
            shieldPoints = 0;
            ChangeShieldBar(shieldPoints);
            playerHealth.DamageToHealth(damage);
        }
    }

    private IEnumerator RecoveryByTime()
    {
        while (true)
        {
            while (shieldPoints < maxShieldPoints)
            {
                if (isDamagedRecently)
                {
                    yield return new WaitForSeconds(waitSecAfterDamage);
                    isDamagedRecently = false;
                }
                RestoreShield(1);
                yield return new WaitForSeconds(waitSecBetweenRestoring);
            }
            yield return null;
        }
    }
}