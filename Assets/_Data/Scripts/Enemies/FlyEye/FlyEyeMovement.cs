using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeMovement : NhoxBehaviour
{
    [SerializeField] protected Rigidbody2D rb;

    public event System.Action<bool> OnTargetDetected;
    [SerializeField] protected DetectionZone attackZone;

    public event System.Action<float> CalculateAttackCooldown;
    public Func<float> GetAttackTimeCooldown;


    public float AttackCooldown
    {
        get => GetAttackTimeCooldown?.Invoke() ?? 0;

        private set
        {
            CalculateAttackCooldown?.Invoke(value);
        }
    }

    [SerializeField] protected bool hasTarget;
    public bool HasTarget
    {
        get => hasTarget;
        private set
        {
            hasTarget = value;
            OnTargetDetected?.Invoke(hasTarget);
        }
    }

    private void Update()
    {
        this.FindTarget();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAttackZone();
        this.LoadRigidbody2d();
    }

    protected virtual void LoadAttackZone()
    {
        if (this.attackZone != null) return;
        this.attackZone = transform.parent.Find("HitboxDetection").GetComponent<DetectionZone>();
        Debug.Log(transform.name + "Load AttackZone", gameObject);
    }

    protected virtual void LoadRigidbody2d()
    {
        if (this.rb != null) return;
        this.rb = GetComponentInParent<Rigidbody2D>();
        Debug.Log(transform.name + "Load Rigidbody2D", gameObject);
    }

    protected virtual void FindTarget()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }
}
