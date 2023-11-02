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

    private bool isVaulting;
    private float vaultSpeed = 6f;
    private float vaultTime = 0.2f;
    private float stayVaultRadius = 0.6f;
    
    [SerializeField]
    private LayerMask mask;
    
    private PlayerStamina playerStamina;
    private float spendPointsWhenRunning = 0.3f;
    private float spendPointsWhenDashing = 10f;
    private float spendPointsWhenVaulting = 20f;

    private void Start()
    {
        if (!isOwned) return;
        rb = GetComponent<Rigidbody2D>();
        playerStamina = GetComponent<PlayerStamina>();
    }

    void Update()
    {
        if (!isOwned) return;
        
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        
        direction.Normalize();
        
        if (isDashing || isVaulting) return;
        
        if (Vaulting())
        {
            RaycastHit2D hit = 
                Physics2D.CircleCast(transform.position, stayVaultRadius, 
                                Vector2.zero, Mathf.Infinity, mask);
            if (isNotVaultable(hit)) return;
            StartCoroutine(Vault(hit.collider));
            playerStamina.SpendStamina(spendPointsWhenVaulting);
        }
        else if (Dashing())
        {
            StartCoroutine(Dash());
            playerStamina.SpendStamina(spendPointsWhenDashing);
        }
        else if (Running())
        {
            rb.velocity = direction * (speed * speedIncreaseFactor);
            playerStamina.SpendStamina(spendPointsWhenRunning);
        }
        else
        {
            rb.velocity = direction * speed;
        }
    }

    private bool Running() => Input.GetKey(KeyCode.LeftShift) && playerStamina.GetStaminaPoints() > 0;

    private bool Vaulting()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || !(playerStamina.GetStaminaPoints() > 0)) return false;
        RaycastHit2D hit =
            Physics2D.CircleCast(transform.position, stayVaultRadius,
                Vector2.zero, Mathf.Infinity, mask);
        return !isNotVaultable(hit);
    }

    private bool isNotVaultable(RaycastHit2D hit) =>
        !hit || LayerMask.LayerToName(hit.collider.gameObject.layer) != "Vaultable";

    private bool Dashing() => 
        Input.GetKeyDown(KeyCode.Space) && direction != Vector2.zero && playerStamina.GetStaminaPoints() > 0;

    private IEnumerator Dash()
    {
        isDashing = true;
        rb.velocity = direction * dashSpeed;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
    }

    private IEnumerator Vault(Collider2D collider)
    {
        isVaulting = true;
        rb.GetComponent<CircleCollider2D>().enabled = false;
        Vector2 vaultDirection = collider.transform.position - transform.position;
        rb.velocity = vaultDirection * vaultSpeed;
        yield return new WaitForSeconds(vaultTime);
        rb.GetComponent<CircleCollider2D>().enabled = true;
        isVaulting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isOwned || !isDashing) return;
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, stayVaultRadius, 
                Vector2.zero, Mathf.Infinity, mask);
        if (isNotVaultable(hit)) return;
        StopCoroutine(Dash());
        isDashing = false;
        StartCoroutine(Vault(hit.collider));
        playerStamina.SpendStamina(spendPointsWhenVaulting);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, dashSpeed*dashTime);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, stayVaultRadius);
    }
}
