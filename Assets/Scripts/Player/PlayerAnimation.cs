using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    private string currentAnimation;

    private Vector2 direction;

    private PlayerHealth health;

    private void Start()
    {
        health = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        GetDirections();
        if (health.GetHealth() <= 0)
        {
            ChangeAnimation("pers_death");
            Destroy(anim, 0.8f);
            Destroy(gameObject.GetComponent<PlayerMovement>());
            Destroy(gameObject.GetComponent<PlayerHealth>());
            Destroy(gameObject.GetComponent<PlayerAnimation>());
        }
        else if (direction == Vector2.zero)
        {
            ChangeAnimation("idle");
        }
        else if (direction == Vector2.up)
        {
            ChangeAnimation("running_back");
        }
        else
        {
            ChangeAnimation("running_right");
        }
    }

    private void GetDirections()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
    }

    private void ChangeAnimation(string animation)
    {
        if (currentAnimation == animation) return;
        anim.Play(animation);
        currentAnimation = animation;
    }
}