using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    private Animator anim;
    private string currentAnimation;

    private Vector2 startVector;
    private Vector2 endVector;

    private Transform player;
    [SerializeField, Range(0, 5)] private float biteDistanse = 1.5f;

    private void Start()
    {
        endVector = transform.position;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        startVector = transform.position;
        if (startVector != endVector)
        {
            endVector = startVector;
            ChangeAnimation("zombie_idle");
        }
        else if (Vector2.Distance(transform.position, player.position) <= biteDistanse)
        {
            ChangeAnimation("zombie_bite");
        }
        else
        {
            ChangeAnimation("zombie_stay");
        }

    }

    private void ChangeAnimation(string animation)
    {
        if (currentAnimation == animation) return;
        anim.Play(animation);
        currentAnimation = animation;
    }
}
