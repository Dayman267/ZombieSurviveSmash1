using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] private float speed;
    [SerializeField] [Range(0, 5)] private float stoppingDistance = 2;
    [SerializeField] [Range(0, 50)] private float findDistance = 8;
    private float direction;

    private bool facingRight = true;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        direction = player.position.x - transform.position.x;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance &&
            Vector2.Distance(transform.position, player.position) < findDistance)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (facingRight == false && direction > 0)
            Flip();
        else if (facingRight && direction < 0) Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        var scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}