using UnityEngine;
using Mirror;

public class LighterRotate : NetworkBehaviour
{
    private Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (!isLocalPlayer) return;
        
        Vector3 difference = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotate = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotate-90);
    }
}
