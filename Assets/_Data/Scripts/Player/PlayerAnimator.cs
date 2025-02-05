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
        anim.SetBool("isMoving", isMoving);
    }

    public void UpdateRunning(bool isRunning)
    {
        anim.SetBool("isRunning", isRunning);
    }
}
