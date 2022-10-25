using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{
    public Transform camera;
    public Transform player;
    private float speed = 3.0f;
    
    void Update()
    {
        Vector3 position = player.position;
        position.z = -1.0F;
        transform.position = Vector3.MoveTowards(transform.position, position, speed);
    }
}
