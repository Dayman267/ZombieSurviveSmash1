using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class TeleportInPlayer : NetworkBehaviour
{
    public Button bt;

    private void Start()
    {
        if(!isLocalPlayer) return;
        bt = GameObject.FindWithTag("StartButton").GetComponent<Button>();
        bt.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!collider.gameObject.CompareTag("Teleport")) return;
        bt.gameObject.SetActive(true);
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(!collider.gameObject.CompareTag("Teleport")) return;
        bt.gameObject.SetActive(false);
    }
}
