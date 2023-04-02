using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public float speed = 100f;
    private Vector2 direction;
    [SerializeField] private Rigidbody2D rb;
    private bool facingRight = true;

    private void Start()
    {
        
    }

    void Update()
    {
        if (hasAuthority)
        {
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = Input.GetAxisRaw("Vertical");
            //GameObject.FindGameObjectWithTag("Zombie").GetComponent<ZombieMovement>().player = transform;
        }
    }

    private void FixedUpdate()
    {
        if (hasAuthority)
        {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            if (facingRight == false && direction.x > 0)
            {
                Flip();
            }
            else if (facingRight == true && direction.x < 0)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        if (hasAuthority)
        {
            facingRight = !facingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }
}
