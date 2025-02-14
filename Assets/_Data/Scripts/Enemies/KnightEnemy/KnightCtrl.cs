using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightCtrl : EnemyAbstract
{
    private static KnightCtrl instance;
    public static KnightCtrl Instance => instance;

    [SerializeField] protected KnightMovement knightMovement;
    public KnightMovement KnightMovement => knightMovement;

    [SerializeField] protected KnightAnimator knightAnimator;
    public KnightAnimator KnightAnimator => knightAnimator;

    protected override void Awake()
    {
        base.Awake();
        if (KnightCtrl.instance != null)
        {
            Debug.LogError("Only 1 instance of KnightCtrl is allowed");
        }

        KnightCtrl.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadKnightMovement();
        this.LoadKnightAnimator();
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

    
}
