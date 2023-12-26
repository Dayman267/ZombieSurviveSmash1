using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 100f;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 direction;
    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        if (facingRight == false && direction.x > 0)
            Flip();
        else if (facingRight && direction.x < 0) Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        var scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}