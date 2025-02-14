using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeCtrl : EnemyAbstract
{
    private static FlyEyeCtrl instance;
    public static FlyEyeCtrl Instance => instance;

    [SerializeField] protected FlyEyeMovement flyEyeMovement;
    public FlyEyeMovement FlyEyeMovement => flyEyeMovement;

    [SerializeField] protected FlyEyeAnimator flyEyeAnimator;
    public FlyEyeAnimator FlyEyeAnimator => flyEyeAnimator;

    protected override void Awake()
    {
        base.Awake();
        if (FlyEyeCtrl.instance != null)
        {
            Debug.LogError("Only 1 instance of FlyEyeCtrl is allowed");
        }

        FlyEyeCtrl.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadFlyEyeMovement();
        this.LoadFlyEyeAnimator();
    }

    protected virtual void LoadFlyEyeMovement()
    {
        if (this.flyEyeMovement != null) return;
        flyEyeMovement = GetComponentInChildren<FlyEyeMovement>();
        Debug.Log(transform.name + " Load FlyEyeMovement", gameObject);
    }

    protected virtual void LoadFlyEyeAnimator()
    {
        if (this.flyEyeAnimator != null) return;
        flyEyeAnimator = GetComponentInChildren<FlyEyeAnimator>();
        Debug.Log(transform.name + " Load FlyEyeAnimator", gameObject);
    }
}
 