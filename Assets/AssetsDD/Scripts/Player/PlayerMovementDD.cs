using System.Collections;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;
using UnityEditor;

public class PlayerMovementDD : NetworkBehaviour
{
    private Vector2 direction;
    private Rigidbody2D rb;
    
    [Header("Running")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speedIncreaseFactor = 1.5f;
    [SerializeField] private float spendPointsWhenRunning = 0.3f;

    private bool isDashing;
    [Header("Dashing")]
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashTime = 0.5f;
    [SerializeField] private float spendPointsWhenDashing = 10f;

    private bool isVaulting;
    [Header("Vaulting")]
    [SerializeField] private float vaultSpeed = 6f;
    [SerializeField] private float vaultTime = 0.2f;
    [SerializeField] private float stayVaultRadius = 0.6f;
    [SerializeField] private float spendPointsWhenVaulting = 20f;
    
    [SerializeField]
    private LayerMask mask;
    
    private PlayerStamina playerStamina;

    private void Start()
    {
        // #if !UNITY_EDITOR
        //     Application.Quit();
        // #endif
        // #if UNITY_EDITOR
        //     EditorApplication.isPlaying = false;
        // #endif
        
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
            if (!isVaultable(hit)) return;
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

    private bool Running() => Input.GetKey(KeyCode.LeftShift) && direction != Vector2.zero && playerStamina.GetStaminaPoints() > 0;

    private bool Vaulting()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || !(playerStamina.GetStaminaPoints() > 0)) return false;
        RaycastHit2D hit =
            Physics2D.CircleCast(transform.position, stayVaultRadius,
                Vector2.zero, Mathf.Infinity, mask);
        return isVaultable(hit);
    }

    private bool isVaultable(RaycastHit2D hit) =>
        !hit.collider.IsUnityNull() && LayerMask.LayerToName(hit.collider.gameObject.layer) == "Vaultable";

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
        if (!isVaultable(hit)) return;
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
