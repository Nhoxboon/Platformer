using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : NhoxBehaviour
{
    [SerializeField] protected Animator anim;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    protected virtual void LoadAnimator()
    {
        if (this.anim != null) return;
        this.anim = GetComponentInParent<Animator>();
        Debug.Log(transform.name + " Load Animator", gameObject);
    }

    public void UpdateMovement(bool isMoving)
    {
        anim.SetBool(AnimationStrings.isMoving, isMoving);
    }

    public void UpdateRunning(bool isRunning)
    {
        anim.SetBool(AnimationStrings.isRunning, isRunning);
    }

    public void UpdateJump()
    {
        anim.SetTrigger(AnimationStrings.jump);
    }

    public void Attack()
    {
        anim.SetTrigger(AnimationStrings.attack);
    }

    public void CheckGrounded(bool isGrounded)
    {
        anim.SetBool(AnimationStrings.isGrounded, isGrounded);
    }

    public void CheckJumpOrFall(float yVelocity)
    {
        anim.SetFloat(AnimationStrings.yVelocity, yVelocity);
    }

    public void IsOnWall(bool isOnWall)
    {
        anim.SetBool(AnimationStrings.isOnWall, isOnWall);
    }

    public void IsOnCelling(bool isOnCelling)
    {
        anim.SetBool(AnimationStrings.isOnCelling, isOnCelling);
    }

    public bool CanMove()
    {
        return anim.GetBool(AnimationStrings.canMove);
    }
}
