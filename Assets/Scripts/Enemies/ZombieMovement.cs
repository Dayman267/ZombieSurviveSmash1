using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float speed;
    private Transform player;
    [SerializeField, Range(0, 5)] private float biteDistance = 3;

    private bool facingRight = true;
    private float direction;
    private float startX;
    private float endX = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        startX = transform.position.x;
        if (startX != endX)
        {
            direction = startX - endX;
            endX = startX;
        }
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.position) > biteDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        if (facingRight == false && direction > 0)
        {
            Flip();
        }
        else if (facingRight == true && direction < 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

        }
    }
}
