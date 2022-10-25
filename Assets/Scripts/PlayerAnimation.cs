using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    private string currentAnimation;

    private Vector2 direction;

    void Update()
    {
        GetDirections();

        if (direction == Vector2.zero) ChangeAnimation("idle");
        else if (direction == Vector2.up) ChangeAnimation("running_back");
        else ChangeAnimation("running_right");
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
