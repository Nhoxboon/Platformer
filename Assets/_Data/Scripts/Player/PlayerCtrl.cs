using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : NhoxBehaviour
{
    private static PlayerCtrl instance;
    public static PlayerCtrl Instance => instance;

    [SerializeField] protected PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement => playerMovement;

    [SerializeField] protected PlayerAnimator playerAnimator;
    public PlayerAnimator PlayerAnimator => playerAnimator;

    [SerializeField] protected TouchingDirections touchingDirections;
    public TouchingDirections TouchingDirections => touchingDirections;

    [SerializeField] protected Damageable damageable;
    public Damageable Damageable => damageable;

    protected override void Awake()
    {
        base.Awake();
        if(PlayerCtrl.instance != null)
        {
            Debug.LogError("Only 1 instance of PlayerCtrl is allowed");
        }

        PlayerCtrl.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerMovement();
        this.LoadPlayerAnimator();
        this.LoadTouchingDirections();
        this.LoadDamageable();
    }

    protected virtual void LoadPlayerMovement()
    {
        if (this.playerMovement != null) return;
        playerMovement = GetComponentInChildren<PlayerMovement>();
        Debug.Log(transform.name + " Load PlayerMovement", gameObject);
    }

    protected virtual void LoadPlayerAnimator()
    {
        if (this.playerAnimator != null) return;
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
        Debug.Log(transform.name + " Load PlayerAnimator", gameObject);
    }

    protected virtual void LoadTouchingDirections()
    {
        if (this.touchingDirections != null) return;
        touchingDirections = GetComponentInChildren<TouchingDirections>();
        Debug.Log(transform.name + "Load TouchingDirections", gameObject);
    }

    protected virtual void LoadDamageable()
    {
        if (this.damageable != null) return;
        this.damageable = GetComponentInChildren<Damageable>();
        Debug.Log(transform.name + "Load Damageable", gameObject);
    }
}
