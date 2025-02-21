using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimator : NhoxBehaviour
{
    [SerializeField] protected Animator anim;

    protected override void Start()
    {
        KnightCtrl.Instance.KnightMovement.OnTargetDetected += CheckTarget;
        KnightCtrl.Instance.Damageable.OnAliveStateChanged += HandleAliveState;
        KnightCtrl.Instance.Damageable.OnHitStateChanged += HandleHitState;
        KnightCtrl.Instance.KnightMovement.CalculateAttackCooldown += HandleAttackCooldown;
        KnightCtrl.Instance.Damageable.GetIsHitFromAnimator = () => anim.GetBool(AnimationStrings.isHit);
        KnightCtrl.Instance.KnightMovement.GetAttackTimeCooldown = () => anim.GetFloat(AnimationStrings.attackCooldown);
    }

    protected virtual void OnDestroy()
    {
        KnightCtrl.Instance.KnightMovement.OnTargetDetected -= CheckTarget;
        KnightCtrl.Instance.Damageable.OnAliveStateChanged -= HandleAliveState;
        KnightCtrl.Instance.Damageable.OnHitStateChanged -= HandleHitState;
        KnightCtrl.Instance.KnightMovement.CalculateAttackCooldown -= HandleAttackCooldown;
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
