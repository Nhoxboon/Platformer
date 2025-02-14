using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeAnimator : NhoxBehaviour
{
    [SerializeField] protected Animator anim;

    protected override void Start()
    {
        base.Start();
        FlyEyeCtrl.Instance.FlyEyeMovement.OnTargetDetected += CheckTarget;
        FlyEyeCtrl.Instance.FlyEyeMovement.CalculateAttackCooldown += HandleAttackCooldown;
        FlyEyeCtrl.Instance.Damageable.OnAliveStateChanged += HandleAliveState;
        FlyEyeCtrl.Instance.Damageable.OnHitStateChanged += HandleHitState;
        FlyEyeCtrl.Instance.FlyEyeMovement.GetAttackTimeCooldown = () => anim.GetFloat(AnimationStrings.attackCooldown);
        FlyEyeCtrl.Instance.Damageable.GetIsHitFromAnimator = () => anim.GetBool(AnimationStrings.isHit);


    }

    protected virtual void OnDestroy()
    {
        FlyEyeCtrl.Instance.FlyEyeMovement.OnTargetDetected -= CheckTarget;
        FlyEyeCtrl.Instance.FlyEyeMovement.CalculateAttackCooldown -= HandleAttackCooldown;
        FlyEyeCtrl.Instance.Damageable.OnAliveStateChanged -= HandleAliveState;
        FlyEyeCtrl.Instance.Damageable.OnHitStateChanged -= HandleHitState;
    }

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

    public virtual void CheckTarget(bool hasTarget)
    {
        anim.SetBool(AnimationStrings.hasTarget, hasTarget);
    }

    public virtual void HandleAliveState(bool isAlive)
    {
        anim.SetBool(AnimationStrings.isAlive, isAlive);
    }

    public virtual void HandleHitState(bool isHit)
    {
        anim.SetBool(AnimationStrings.isHit, isHit);
    }

    public virtual void HandleAttackCooldown(float attackCooldown)
    {
        anim.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(attackCooldown, 0));
    }

    public bool CanMove()
    {
        return anim.GetBool(AnimationStrings.canMove);
    }
}
