using System;
using UnityEngine;
using Mirror;

public class GiveToCameraOwnPosition : NetworkBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void Update()
    {
        if (!isLocalPlayer) return;
        CameraMovement();
    }

    private void CameraMovement()
    {
        cam.transform.localPosition = new Vector3(transform.position.x, transform.position.y, -1f);
    }
}
