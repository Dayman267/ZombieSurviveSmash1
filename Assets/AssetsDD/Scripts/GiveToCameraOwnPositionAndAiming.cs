using System;
using UnityEngine;
using Mirror;

public class GiveToCameraOwnPositionAndAiming : NetworkBehaviour
{
    private Camera cam;
    [SerializeField, Range(0, 10)] private float offset;

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
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Vector3 temp = cam.transform.localPosition;
            temp.x = transform.position.x + (cam.ScreenToViewportPoint(Input.mousePosition).x - 0.5f) * offset;
            temp.y = transform.position.y + (cam.ScreenToViewportPoint(Input.mousePosition).y - 0.5f) * offset;
            cam.transform.localPosition = temp;
        }
        else
        {
            cam.transform.localPosition = new Vector3(transform.position.x, transform.position.y, -1f);
        }
    }
}
