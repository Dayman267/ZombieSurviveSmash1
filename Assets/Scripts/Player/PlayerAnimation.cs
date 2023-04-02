using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PlayerAnimation : NetworkBehaviour
{
    public Animator anim;
    private string currentAnimation;

    private Vector2 direction;

    [SerializeField]private PlayerHealth health;

    private void Start()
    {
        health.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        health.OnDeath -= OnDeath;
    }

    private void OnDeath()
    {
        ChangeAnimation("pers_death");
        Destroy(anim, 0.8f);
    }

    void Update()
    {
        if (hasAuthority)
        {
            GetDirections();
            if (direction == Vector2.zero) ChangeAnimation("idle");
            else if (direction == Vector2.up) ChangeAnimation("running_back");
            else ChangeAnimation("running_right");
        }
    }

    private void GetDirections()
    {
        if (hasAuthority)
        {
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = Input.GetAxisRaw("Vertical");
        }
    }

    private void ChangeAnimation(string animation)
    {
        if (hasAuthority)
        {
            if (currentAnimation == animation) return;
            anim.Play(animation);
            currentAnimation = animation;
        }
    }
}
