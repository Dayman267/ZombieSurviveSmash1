using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private PlayerAnimation animation;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerMovement movement;

    public override void OnStartLocalPlayer()
    {
        Debug.Log("1111");
        FindObjectOfType<Camera>().GetComponent<CameraMovement>().player = transform;
        CmdPlayerCreated();
    }

    [Command]
    public void CmdPlayerCreated()
    {
        health.Sync();
    }
}
