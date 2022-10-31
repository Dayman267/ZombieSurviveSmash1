using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(0, 100)] public float speed = 20f;
    public Rigidbody2D rb;
    public Transform shootPoint;
    public Camera cam;

    void Start()
    {
        cam = Camera.main;
        shootPoint = GameObject.Find("shootPoint").GetComponent<Transform>();
        rb.velocity = (Vector2)(cam.ScreenToWorldPoint(Input.mousePosition) - shootPoint.position).normalized * speed;

        Debug.Log((Vector2)(cam.ScreenToWorldPoint(Input.mousePosition) - shootPoint.position).normalized);
        Debug.Log((Vector2)(cam.ScreenToWorldPoint(Input.mousePosition) - shootPoint.position));
    }
}
