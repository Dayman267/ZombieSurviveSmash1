using System.Collections;
using UnityEngine;
using Mirror;

public class PlayerMovementDD : NetworkBehaviour
{
    private Vector2 direction;
    private Rigidbody2D rb;
    
    private float speed = 5f;
    private float speedIncreaseFactor = 1.5f;

    private bool isDashing;
    private float dashSpeed = 7f;
    private float dashTime = 0.5f;
    
    private PlayerStamina playerStamina;
    private float spendPointsWhenRunning = 0.3f;
    private float spendPointsWhenDashing = 10f;

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
            
            direction.Normalize();
            
            if (isDashing) return;
            
            if (Input.GetKeyDown(KeyCode.Space) && direction != Vector2.zero && playerStamina.GetStaminaPoints() > 0)
            {
                StartCoroutine(Dash());
                playerStamina.SpendStamina(spendPointsWhenDashing);
            }
            else if (Input.GetKey(KeyCode.LeftShift) && playerStamina.GetStaminaPoints() > 0)
            {
                rb.velocity = direction * (speed * speedIncreaseFactor);
                playerStamina.SpendStamina(spendPointsWhenRunning);
            }
            else
            {
                rb.velocity = direction * speed;
            }
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        rb.velocity = direction * (dashSpeed * speedIncreaseFactor);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
    }
}
