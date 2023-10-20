using UnityEngine;
using Mirror;

public class PlayerMovementDD : NetworkBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speedIncreaseFactor = 1.5f;
    private Vector2 direction;
    private Rigidbody2D rb;
    private PlayerStamina playerStamina;
    [SerializeField] private float spendPoints = 1f;

    private void Start()
    {
        if (isOwned)
        {
            rb = GetComponent<Rigidbody2D>();
            playerStamina = GetComponent<PlayerStamina>();
        }
    }

    void Update()
    {
        if (isOwned)
        {
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = Input.GetAxisRaw("Vertical");
        }
    }

    private void FixedUpdate()
    {
        if (isOwned)
        {
            if (Input.GetKey(KeyCode.LeftShift) && playerStamina.GetStaminaPoints() > 0)
            {
                rb.MovePosition(rb.position + direction * (speed * speedIncreaseFactor * Time.fixedDeltaTime));
                playerStamina.SpendStamina(spendPoints);
            }
            else
            {
                rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));
            }
        }
    }
}
