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
        Debug.Log(transform.name + " LoadPlayerAnimator", gameObject);
    }
}
