using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimator : NhoxBehaviour
{
    [SerializeField] protected Animator anim;

    protected override void Start()
    {
        EnemyCtrl.Instance.KnightMovement.OnTargetDetected += CheckTarget;
        EnemyCtrl.Instance.Damageable.OnAliveStateChanged += HandleAliveState;
    }

    protected virtual void OnDestroy()
    {
        EnemyCtrl.Instance.KnightMovement.OnTargetDetected -= CheckTarget;
        EnemyCtrl.Instance.Damageable.OnAliveStateChanged -= HandleAliveState;
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

    public void HandleAliveState(bool isAlive)
    {
        anim.SetBool(AnimationStrings.isAlive, isAlive);
    }

    public bool CanMove()
    {
        return anim.GetBool(AnimationStrings.canMove);
    }
}
