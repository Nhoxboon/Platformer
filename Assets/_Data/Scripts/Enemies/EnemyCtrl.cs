using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : NhoxBehaviour
{
    private static EnemyCtrl instance;
    public static EnemyCtrl Instance => instance;

    [SerializeField] protected KnightMovement knightMovement;
    public KnightMovement KnightMovement => knightMovement;

    [SerializeField] protected KnightAnimator knightAnimator;
    public KnightAnimator KnightAnimator => knightAnimator;

    [SerializeField] protected TouchingDirections touchingDirections;
    public TouchingDirections TouchingDirections => touchingDirections;

    [SerializeField] protected DetectionZone detectionZone;
    public DetectionZone DetectionZone => detectionZone;

    [SerializeField] protected Damageable damageable;
    public Damageable Damageable => damageable;

    protected override void Awake()
    {
        base.Awake();
        if (EnemyCtrl.instance != null)
        {
            Debug.LogError("Only 1 instance of EnemyCtrl is allowed");
        }

        EnemyCtrl.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadKnightMovement();
        this.LoadKnightAnimator();
        this.LoadTouchingDirections();
        this.LoadDetectionZone();
        this.LoadDamageable();
    }

    protected virtual void LoadKnightMovement()
    {
        if (this.knightMovement != null) return;
        knightMovement = GetComponentInChildren<KnightMovement>();
        Debug.Log(transform.name + " Load KnightMovement", gameObject);
    }

    protected virtual void LoadKnightAnimator()
    {
        if (this.knightAnimator != null) return;
        knightAnimator = GetComponentInChildren<KnightAnimator>();
        Debug.Log(transform.name + " Load KnightAnimator", gameObject);
    }

    protected virtual void LoadTouchingDirections()
    {
        if (this.touchingDirections != null) return;
        touchingDirections = GetComponentInChildren<TouchingDirections>();
        Debug.Log(transform.name + "Load TouchingDirections", gameObject);
    }

    protected virtual void LoadDetectionZone()
    {
        if (this.detectionZone != null) return;
        detectionZone = GetComponentInChildren<DetectionZone>();
        Debug.Log(transform.name + "Load DetectionZone", gameObject);
    }

    protected virtual void LoadDamageable()
    {
        if (this.damageable != null) return;
        this.damageable = GetComponentInChildren<Damageable>();
        Debug.Log(transform.name + "Load Damageable", gameObject);
    }
}
