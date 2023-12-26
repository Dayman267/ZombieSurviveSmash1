using UnityEngine;

public class ShootPointRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera cam;
    private Vector2 mousePos;

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = playerRb.position;
    }

    private void FixedUpdate()
    {
        var lookDir = mousePos - playerRb.position;
        var angle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
        rb.rotation = -angle;
    }
}