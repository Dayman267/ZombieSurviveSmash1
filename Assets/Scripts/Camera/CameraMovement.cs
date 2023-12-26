using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    [SerializeField] [Range(0, 10)] private float offset;

    private void Update()
    {
        var temp = transform.position;
        temp.x = player.position.x + (Camera.main.ScreenToViewportPoint(Input.mousePosition).x - 0.5f) * offset;
        temp.y = player.position.y + (Camera.main.ScreenToViewportPoint(Input.mousePosition).y - 0.5f) * offset;
        transform.position = temp;
    }
}