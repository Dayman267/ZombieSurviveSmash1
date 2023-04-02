using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDBehaviour : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private StaticInventoryDisplay staticInventoryDisplay;

    public void SetPlayer(PlayerView playerView)
    {
        healthBar.SetPlayerHealth(playerView.Health);
        staticInventoryDisplay.SetPlayer(playerView);
    }


}
