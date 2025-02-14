using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbstract : NhoxBehaviour
{
    [SerializeField] protected TouchingDirections touchingDirections;
    public TouchingDirections TouchingDirections => touchingDirections;

    [SerializeField] protected DetectionZone detectionZone;
    public DetectionZone DetectionZone => detectionZone;

    [SerializeField] protected Damageable damageable;
    public Damageable Damageable => damageable;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTouchingDirections();
        this.LoadDetectionZone();
        this.LoadDamageable();
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
