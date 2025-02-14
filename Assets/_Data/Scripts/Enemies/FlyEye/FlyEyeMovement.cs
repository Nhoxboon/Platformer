using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeMovement : NhoxBehaviour
{
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected float flightSpeed = 2f;
    [SerializeField] protected List<Transform> waypoints;
    [SerializeField] protected float waypointReachedDistance = 0.1f;

    public event System.Action<bool> OnTargetDetected;
    [SerializeField] protected DetectionZone attackZone;

    public event System.Action<float> CalculateAttackCooldown;
    public Func<float> GetAttackTimeCooldown;

    Transform nextWaypoint;
    int waypointIndex = 0;


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

    protected override void Start()
    {
        base.Start();
        nextWaypoint = waypoints[waypointIndex];
    }

    private void Update()
    {
        this.FindTarget();
    }

    private void FixedUpdate()
    {
        this.Move();
        this.Death();
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

    protected virtual void Move()
    {
        if (FlyEyeCtrl.Instance.Damageable.IsAlive)
        {
            if (FlyEyeCtrl.Instance.FlyEyeAnimator.CanMove())
            {
                this.FlyThroughPoint();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    protected virtual void FlyThroughPoint()
    {
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.parent.position).normalized;

        float distance = Vector2.Distance(nextWaypoint.position, transform.parent.position);

        rb.velocity = directionToWaypoint * flightSpeed;
        this.UpdateDirection();

        if (distance <= waypointReachedDistance)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Count)
            {
                waypointIndex = 0;
            }
            nextWaypoint = waypoints[waypointIndex];
        }
    }

    protected virtual void FindTarget()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    protected virtual void UpdateDirection()
    {
        if(transform.parent.localScale.x > 0)
        {
            if(rb.velocity.x < 0)
            {
                transform.parent.localScale = new Vector3(-1 * transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                transform.parent.localScale = new Vector3(-1 * transform.parent.localScale.x, transform.parent.localScale.y, transform.parent.localScale.z);
            }
        }
    }

    public void Death()
    {
        if(!FlyEyeCtrl.Instance.Damageable.IsAlive)
        {
            rb.gravityScale = 2;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
