using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerView : NetworkBehaviour
{
    [SerializeField] private PlayerAnimation animation;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private InventoryHolder inventoryHolder;

    public PlayerAnimation Animation => animation;
    public PlayerHealth Health => health;
    public PlayerMovement Movement => movement;
    public InventoryHolder InventoryHolder => inventoryHolder;

    public override void OnStartLocalPlayer()
    {
        FindObjectOfType<Camera>().GetComponent<CameraMovement>().player = transform;
        var hud = FindObjectOfType<HUDBehaviour>();
        hud.SetPlayer(this);
        CmdPlayerCreated();
    }

    [Command]
    public void CmdPlayerCreated()
    {
        health.Sync();
    }
}
