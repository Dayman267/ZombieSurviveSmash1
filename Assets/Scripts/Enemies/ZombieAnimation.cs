using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    [SerializeField] [Range(0, 5)] private float biteDistanse = 1.5f;

    [SerializeField] private float biteDamage;
    private Animator anim;
    private string currentAnimation;
    private Vector2 endVector;

    private ZombieHealth health;

    private Transform player;
    private PlayerHealth playerHealth;

    private Vector2 startVector;

    private void Start()
    {
        endVector = transform.position;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        health = GetComponent<ZombieHealth>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void FixedUpdate()
    {
        startVector = transform.position;
        if (health.GetHealth() <= 0)
        {
            ChangeAnimation("zombie_death");
            Destroy(gameObject.GetComponent<ZombieMovement>());
            Destroy(gameObject.GetComponent<CapsuleCollider2D>());
            Destroy(gameObject.GetComponent<Animator>(), 0.8f);
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            Destroy(gameObject.GetComponent<PositionRendererSorter>());
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject.GetComponent<ZombieHealth>());
            Destroy(gameObject.GetComponent<ZombieAnimation>());
        }
        else if (startVector != endVector)
        {
            endVector = startVector;
            ChangeAnimation("zombie_idle");
        }
        else if (Vector2.Distance(transform.position, player.position) <= biteDistanse)
        {
            ChangeAnimation("zombie_bite");
            playerHealth.TakeDamage(biteDamage);
        }
        else
        {
            ChangeAnimation("zombie_stay");
        }
    }

    /*private IEnumerator BiteCoroutine()
    {
        if(Vector2.Distance(transform.position, player.position) <= biteDistanse)
        {
            playerHealth.TakeDamage(biteDamage);
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForFixedUpdate();
    }*/

    private void ChangeAnimation(string animation)
    {
        if (currentAnimation == animation) return;
        anim.Play(animation);
        currentAnimation = animation;
    }
}