using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float speed;
    /* */[SerializeField]private Transform player;
    [SerializeField, Range(0, 5)] private float stoppingDistance = 2;
    [SerializeField, Range(0, 50)] private float findDistance = 8;

    private bool facingRight = true;
    private float direction;

    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        direction = player.position.x - transform.position.x;
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance &&
            Vector2.Distance(transform.position, player.position) < findDistance)
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
}
